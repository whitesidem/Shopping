using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shopping.Interfaces;
using Shopping.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace Shopping
{
    [Binding]
    public class ShoppingListSteps : IReceiptPrinter
    {
        private ICheckoutTill _checkoutTill;


        [BeforeScenario] 
        public  void BeforeScenario()
        {
        }

        [Given(@"the following pricing rules")]
        public void GivenTheFollowingPricingRules(Table table)
        {
            var pricingRules = new PricingRules();
            var rules = table.CreateDynamicSet().ToList();
            foreach (dynamic rule in rules)
            {
                pricingRules.AddRule((string)rule.Sku, (Decimal)rule.Price, (string)rule.Rule);
            }
            _checkoutTill = new CheckoutTill(pricingRules, this);
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
            _checkoutTill.OutputReceiptTotal();
        }
        
        [Then(@"the total price should be (.*)")]
        public void ThenTheTotalPriceShouldBe(Decimal expectedTotal)
        {
            var total = ScenarioContext.Current["total"];
            Assert.That(total, Is.EqualTo(expectedTotal));
        }

        [When(@"I request receipt items")]
        public void WhenIRequestReceiptItems()
        {
            _checkoutTill.OutputRecieptItems();
        }

        [Then(@"will contain (.*) receipt items")]
        public void ThenWillContainReceiptItems(int qty)
        {
            var actualItems = ScenarioContext.Current.Get<IList<ReceiptItem>>("Receptitems");
            Assert.That(qty, Is.EqualTo(actualItems.Count));
        }


        [Then(@"the following will be output:")]
        public void ThenTheFollowingWillBeOutput(Table table)
        {
            var expectedItems = table.CreateDynamicSet().ToList();
            var actualItems = ScenarioContext.Current.Get<IList<ReceiptItem>>("Receptitems");
            for (var i = 0; i < expectedItems.Count; i++)
            {
                Assert.That(expectedItems[i].Desc, Is.EqualTo(actualItems[i].Desc));
                Assert.That(expectedItems[i].Price, Is.EqualTo(actualItems[i].Price));
            }
        }


        #region IReceiptPrinter sideway shunt

        public void ListReciptitems(List<ReceiptItem> receiptItems)
        {
            ScenarioContext.Current["Receptitems"] = receiptItems;
        }

        public void TotalPrice(decimal totalPrice)
        {
            ScenarioContext.Current["total"] = totalPrice;
        }

        #endregion IReceiptPrinter sideway shunt

    }
}
