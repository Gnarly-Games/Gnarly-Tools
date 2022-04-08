using UnityEngine;

namespace Sources.Utility
{
    public static class DetectionUtility
    {
        public static (bool isInside, Vector3 position) CheckVisionPosition(Transform transform, Camera camera,
            Canvas canvas, float offset)
        {
            var cameraDirection = transform.position - camera.transform.position;
            var angle = Vector3.Angle(camera.transform.forward, cameraDirection);
            angle = Mathf.Clamp(angle, 0f, camera.fieldOfView / 2f);
            var result = canvas.planeDistance / Mathf.Cos(angle * Mathf.Deg2Rad);
            var targetPosition = camera.transform.position + cameraDirection.normalized * result;
            targetPosition = Clamp(targetPosition, canvas, offset, out var isInside);
            return (isInside, targetPosition);
        }

        private static Vector3 Clamp(Vector3 targetPosition, Canvas canvas,
            float offset, out bool isInside)
        {
            isInside = true;
            var rect = canvas.transform as RectTransform;
            var corners = new Vector3[4];
            rect.GetWorldCorners(corners);

            var maxX = corners[2].x - offset;
            var minX = corners[0].x + offset;

            var maxY = corners[1].y;
            var minY = corners[0].y;

            var maxZ = corners[1].z - offset;
            var minZ = corners[0].z + offset;

            if (targetPosition.x > maxX)
            {
                targetPosition.x = maxX;
                isInside = false;
            }
            else if (targetPosition.x < minX)
            {
                targetPosition.x = minX;
                isInside = false;
            }

            if (targetPosition.y > maxY)
            {
                targetPosition.y = maxY;
                isInside = false;
            }
            else if (targetPosition.y < minY)
            {
                targetPosition.y = minY;
                isInside = false;
            }

            if (targetPosition.z > maxZ)
            {
                targetPosition.z = maxZ;

                if (targetPosition.y > maxY)
                {
                    targetPosition.y = maxY;
                }

                isInside = false;
            }
            else if (targetPosition.z < minZ)
            {
                targetPosition.z = minZ;

                if (targetPosition.y > minY)
                {
                    targetPosition.y = minY;
                }

                isInside = false;
            }
            else
            {
                var yRange = maxY - minY;
                var zRange = maxZ - minZ;
                var interpolateY = (((targetPosition.z - minZ) * yRange) / zRange) + minY;
                targetPosition.y = interpolateY;
            }

            return targetPosition;
        }
    }
}