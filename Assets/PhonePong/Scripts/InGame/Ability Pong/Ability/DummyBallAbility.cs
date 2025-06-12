// Unity
using UnityEngine;

// PhonePong
using PhonePong.Enum;

public class DummyBallAbility : Ability
{
    [SerializeField] private GameObject dummyBallPrefab;
    [SerializeField] private PlayerEnum racketPlayerEnum;

    [SerializeField] private Vector2[] dummyBallSpawnPosOffsetArray;

    public override void Excute(AbilityRacket racket)
    {
        racket.SetAbility((AbilityBall ball) => UseAbility(ball));

        racketPlayerEnum = racket.PlayerEnum;
    }

    public void UseAbility(AbilityBall ball)
    {
        float dummyBalldirectionX = 0f;
        switch (racketPlayerEnum)
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
            GameObject dummyBall = Instantiate(dummyBallPrefab, (Vector2)ball.transform.position + dummyBallSpawnPosOffsetArray[i], Quaternion.identity);
            dummyBall.GetComponent<DummyBall>().SetVelocity(new Vector2(dummyBalldirectionX, Random.Range(-1f, 1f)));
        }
        
    }
}
