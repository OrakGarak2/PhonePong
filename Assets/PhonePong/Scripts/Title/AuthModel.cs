using System;
using System.Text.RegularExpressions;
using BackEnd;

namespace LegendPingPong.Login
{
    public class AuthModel
    {
        /// <summary>
        /// 이메일의 패턴이 맞는지 확인하는 메서드
        /// </summary>
        /// <param name="email">이메일</param>
        /// <returns>만약 패턴이 맞을 경우 True를 리턴, 패턴이 맞지 않을 경우 False를 리턴</returns>
        public static bool IsValidEmail(string email)
        {
            const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }

        public void Login(string id, string pw, Action<bool, string> callback)
        {
            Backend.BMember.CustomLogin(id, pw, bro =>
            {
                if (bro.IsSuccess()) callback(true, null);
                else
                {
                    string msg = bro.GetStatusCode() switch
                    {
                        "410" => "탈퇴된 계정입니다",
                        "403" => "차단당한 계정입니다",
                        "401" => "아이디 또는 비밀번호가 알맞지 않습니다",
                        var _ => bro.GetMessage()
                    };
                    callback(false, msg);
                }
            });
        }

        public void Register(string id, string pw, string email, Action<bool, string> callback)
        {
            Backend.BMember.CustomSignUp(id, pw, bro1 =>
            {
                if (!bro1.IsSuccess())
                {
                    string msg = bro1.GetStatusCode() switch
                    {
                        "409" => "이미 존재하는 아이디입니다",
                        var _ => bro1.GetMessage()
                    };
                    callback(false, msg);
                    return;
                }
    
                Backend.BMember.UpdateCustomEmail(email, bro2 =>
                {
                    if (!bro2.IsSuccess())
                    {
                        callback(false, bro2.GetMessage());
                    }
                    else callback(true, null);
                });
            });
        }
    }
}

