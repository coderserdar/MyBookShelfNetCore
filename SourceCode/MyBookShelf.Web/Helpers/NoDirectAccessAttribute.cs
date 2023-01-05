using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyBookShelf.Web.Helpers;

/// <summary>
/// Bu sınıf, kullanıcı uygulamanın herhangi bir sayfasına direk olarak ulaşmaya çalıştığında
/// bağlantıyı kabul etmeyerek direk olarak login sayfasına yönlendirme yapmaktadır.
/// Controller içerisindeki direk gitmeyi engellemek istenen sayfaların Action methodlarının
/// üst kısımlarına konuduğunda otomatik olarak engelleme sağlanmış olacaktır.
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class NoDirectAccessAttribute : ActionFilterAttribute
{
    /// <summary>
    /// Kullanıcının adres çubuğuna direkt olarak bir adres yazıp yazmadığını kontrol eden
    /// Eğer bir sayfaya direkt olarak erişmek için adres çubuğuna yazdıysa
    /// Login sayfasına yönlendirme yapmayı sağlayan metottur.
    /// </summary>
    /// <param name="filterContext">Talep Yapılan Adres Bilgisini De İçeren Filtre Bilgisi</param>
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (string.IsNullOrEmpty(filterContext.HttpContext.Session.GetString("ActiveUserId")) && (filterContext.Controller != "HomeController") && !filterContext.ActionDescriptor.DisplayName.Contains("Login"))
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Home", action = "Login" }));
        }
        
    }
}