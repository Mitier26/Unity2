using UnityEngine;

public class Utils 
{
    /// <summary>
    /// ������ �������� ���� �ѷ� ��ġ�� ���Ѵ�.
    /// </summary>
    /// <param name="radius">���� ������</param>
    /// <param name="angle">����</param>
    /// <returns>���� ������, ������ �ش��ϴ� �ѷ� ��ġ</returns>
   public static Vector3 GetPositionFromAngle(float radius, float angle)
    {
        Vector3 position = Vector3.zero;

        angle = DegreeToRadian(angle);

        position.x = Mathf.Cos(angle) * radius;
        position.y = Mathf.Sin(angle) * radius;

        return position;
    }

    /// <summary>
    /// Degree ���� Radian ������ ��ȯ
    /// 1���� "PI / 180" radian
    /// angle�� "PI / 180 * angle" radian
    /// </summary>
    public static float DegreeToRadian(float angle)
    {
        return Mathf.PI * angle / 180;
    }
    /// <summary>
    /// Radian ���� Degree ������ ��ȯ
    /// 1 radian = "180 / PI"��
    /// angleRadian�� "180/PI * angle"��
    /// </summary>
    public static float RadianToDegree(float angle)
    {
        return angle * (180 / Mathf.PI);
    }
}
