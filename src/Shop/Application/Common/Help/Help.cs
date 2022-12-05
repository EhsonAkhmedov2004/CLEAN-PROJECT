

namespace Application.Common.Help
{
    public record class Response<T>(T response,int statusCode) { };
    public record class Money (int money) { };
    public class Helper
    {

        public static Response<T> Respond<T>(T response,int statusCode)
        {
            return new Response<T>(response, statusCode);
            
        }
        public static Money Money(int money)
        {
            return new Money(money);
        }
        
    }
}
