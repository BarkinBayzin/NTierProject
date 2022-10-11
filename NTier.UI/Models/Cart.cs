using NTier.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NTier.UI.Models
{
    public class Cart
    {
        private Dictionary<Guid, Product> _cart = null;

        public Cart()
        {
            _cart = new Dictionary<Guid, Product>();
        }

        public List<Product> CartProductList 
        {
            get
            {
                return _cart.Values.ToList();
            }
        }

        public void AddCart(Product item)
        {
            //Eğer o id'ye ait bir item yoksa id ve itemi sözlük(dictionary) içerisine değeri ile atıyoruz.
            if (!_cart.ContainsKey(item.Id))
            {
                _cart.Add(item.Id, item);
            }
            else
                _cart[item.Id].Quantity += 1; //Eğer o id varsa satın alınacak(sepete eklenecek) miktarı bir arttırır.
        }


        public void RemoveCart(Guid id)
        {
            _cart.Remove(id);
        }


        public void Decrease(Guid id)
        {
            //Sepetten bir azaltma
            _cart[id].Quantity -= 1;
            //Eğer sepette azaltırkan başka o elemandan kalmadı iste tamamen sepetten siliyoruz.
            if (_cart[id].Quantity <= 0)
            {
                _cart.Remove(id);
            }
        }

        public void IncreaseCart(Guid id)
        {
            //Sepetteki ürünün miktarı bir arttır.
            _cart[id].Quantity += 1;
        }
    }
}