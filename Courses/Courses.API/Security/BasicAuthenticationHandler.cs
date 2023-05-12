using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Courses.API.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOption>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOption> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            //1. gelen request'de headers içine 'Authorization' var mı?
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //2. Authorization doğru formatta mı?
            if (!AuthenticationHeaderValue.TryParse(Request.Headers["Authorization"], out AuthenticationHeaderValue headerValue))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //3. Authorization değeri Basic mi?
            if (headerValue.Scheme != "Basic")
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            //4. Kullancı ve şifrenin bulunduğu turkay:123 değerini encode et ve kullanıcı adı ve şifreyi al
            var headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
            var headerValeString = Encoding.UTF8.GetString(headerValueBytes);
            string userName = headerValeString.Split(':')[0];
            string password = headerValeString.Split(':')[1];
            //5. Denetle:

            if (userName != "turkay" && password != "123")
            {
                return Task.FromResult(AuthenticateResult.Fail("Hatalı kullanıcı adı ya da şifre"));
            }

            //6. Kişinin rolü gibi uygulama kullanıcısında olmasını istediğiniz ve PAYLAŞILABİLİR verileri oluşturun.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,userName),
                new Claim(ClaimTypes.Role,"Client")

            };

            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));





        }
    }
}
