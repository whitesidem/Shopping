using System;
using System.Linq;
using NUnit.Framework;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Shopping
{
    [Binding]
    public class ShoppingListSteps
    {
        private ICheckoutTill _checkoutTill;


        [BeforeScenario] 
        public  void BeforeScenario()
        {
            _checkoutTill = new CheckoutTill();
        }

        [Given(@"I have (.*) Product '(.*)' with price (.*)")]
        public void GivenIHaveProductWithPrice(int qty, string sku, Decimal price)
        {
            for (var i = 0; i < qty; i++)
            {
                _checkoutTill.AddItem(sku, price);
            }
        }

        [Given(@"I have the following items:")]
        public void GivenIHaveTheFollowingItems(Table table)
        {
            var items = table.CreateDynamicSet().ToList();
            foreach (dynamic item in items)
            {
                _checkoutTill.AddItem((string)item.Sku, (Decimal)item.Price);
            }
            // Get<IEnumerable<dynamic>>("Products")
        }

        
        [When(@"I calculate the total")]
        public void WhenICalculateTheTotal()
        {
            ScenarioContext.Current["total"] = _checkoutTill.CalcTotal();
        }
        
        [Then(@"the total price should be (.*)")]
        public void ThenTheTotalPriceShouldBe(Decimal expectedTotal)
        {
            var total = ScenarioContext.Current["total"];
            Assert.That(total, Is.EqualTo(expectedTotal));
        }
    }
}
