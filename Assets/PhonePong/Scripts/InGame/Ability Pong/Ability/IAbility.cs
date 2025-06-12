namespace PhonePong.InGameInterface
{
    public interface IAbility
    {
        /// <summary>
        /// 능력 사용 메서드
        /// </summary>
        /// <param name="racket">능력을 사용할 라켓</param>
        public void Excute(Racket racket);
    }
}