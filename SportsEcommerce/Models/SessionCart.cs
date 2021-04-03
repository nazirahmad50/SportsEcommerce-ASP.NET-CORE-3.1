using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SportsEcommerce.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SportsEcommerce.Models
{
    public class SessionCart : Cart
    {
        // this method is a factory for creating SessionCart objects and providing them with Isession object so they can store themselves
        public static Cart GetCart(IServiceProvider service)
        {
            // get the session using asp.net core DI
            // i obtain an instance of IHttpContextAccessor service which provides me wth access to an HttpContext object that in turn provides the ISession
            // the indirect approach is required as the session isnt provides as a regular service
            ISession session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionCart cart = session.GetJson<SessionCart>("Cart") ?? new SessionCart();

            cart.Session = session;

            return cart;
            
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Product product, int quantity)
        {
            base.AddItem(product, quantity);
            Session.SetJson("Cart", this);
        }

        public override void RemoveLine(Product product)
        {
            base.RemoveLine(product);
            Session.Remove("Cart");
        }

        public override void Clear()
        {
            base.Clear();
            Session.Remove("Cart");
        }
    }
}
