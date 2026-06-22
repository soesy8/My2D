using UnityEngine;

namespace My2DGame
{
    /// <summary>
    /// 애니메이터 파라미터 이름 리스트 - 전역적 접근
    /// </summary>
    public class AnimationString
    {
        public static string isMove = "IsMove";
        public static string isRun = "IsRun";
        public static string isGrounded = "IsGrounded";
        public static string isWall = "IsWall";
        public static string jumpTrigger = "JumpTrigger";
        public static string yVelocity = "YVelocity";
        public static string attackTrigger = "AttackTrigger";
        public static string cannotMove = "CannotMove";
    }
}