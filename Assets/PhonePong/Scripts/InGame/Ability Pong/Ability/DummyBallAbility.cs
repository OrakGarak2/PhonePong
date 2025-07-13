// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;

public class DummyBallAbility : Ability, IBallAbility
{
    [SerializeField] private GameObject dummyBallPrefab;
    [SerializeField] private PlayerEnum paddlePlayerEnum;

    [SerializeField] private Vector2[] dummyBallSpawnPosOffsetArray;

    public override void Excute(AbilityPaddle paddle)
    {
        paddle.SetAbility(UseAbility);

        paddlePlayerEnum = paddle.PlayerEnum;
    }

    public void UseAbility(AbilityBall ball)
    {
        float dummyBalldirectionX = 0f;
        switch (paddlePlayerEnum)
        {
            case PlayerEnum.Player1:
                dummyBalldirectionX = -1f;
                break;
            case PlayerEnum.Player2:
                dummyBalldirectionX = 1f;
                break;
        }

        for (int i = 0; i < dummyBallSpawnPosOffsetArray.Length; i++)
        {
            DummyBall dummyBall = Instantiate(dummyBallPrefab, (Vector2)ball.transform.position + dummyBallSpawnPosOffsetArray[i], Quaternion.identity)
                .GetComponent<DummyBall>();
            dummyBall.SetDummyBall(ball, new Vector2(dummyBalldirectionX, Random.Range(-1f, 1f)));
        }

        AudioManager.Instance.PlayOneShot(FMODEvents.Instance.generateDummyBall, ball.transform.position);
    }
}
