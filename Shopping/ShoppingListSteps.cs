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

        [Given(@"the following pricing rules")]
        public void GivenTheFollowingPricingRules(Table table)
        {
            var rules = table.CreateDynamicSet().ToList();
            foreach (dynamic rule in rules)
            {
                _checkoutTill.AddPricingRule((string)rule.Sku, (Decimal)rule.Price, (string)rule.Rule);
            }
        }



        [Given(@"I have (.*) Product '(.*)'")]
        public void GivenIHaveProductWithPrice(int qty, string sku)
        {
            for (var i = 0; i < qty; i++)
            {
                _checkoutTill.AddItem(sku);
            }
        }

        [Given(@"I have the following items:")]
        public void GivenIHaveTheFollowingItems(Table table)
        {
            var items = table.CreateDynamicSet().ToList();
            foreach (dynamic item in items)
            {
                _checkoutTill.AddItem((string)item.Sku);
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
