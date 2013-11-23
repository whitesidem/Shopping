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
                _checkoutTill.ScanItem(sku);
            }
        }

        [Given(@"I have the following items:")]
        public void GivenIHaveTheFollowingItems(Table table)
        {
            var items = table.CreateDynamicSet().ToList();
            foreach (dynamic item in items)
            {
                _checkoutTill.ScanItem((string)item.Sku);
            }
        }

        
        [When(@"I calculate the total")]
        public void WhenICalculateTheTotal()
        {
            _checkoutTill.OutputReceipt();
        }
        
        [Then(@"the total price should be (.*)")]
        public void ThenTheTotalPriceShouldBe(Decimal expectedTotal)
        {
            var total = ScenarioContext.Current["total"];
            Assert.That(expectedTotal, Is.EqualTo(total));
        }

        [When(@"I request receipt items")]
        public void WhenIRequestReceiptItems()
        {
            _checkoutTill.OutputReceipt();
        }

        [Then(@"will contain (.*) receipt items")]
        public void ThenWillContainReceiptItems(int qty)
        {
            var actualItems = ScenarioContext.Current.Get<IList<ReceiptItem>>("Receptitems");
            Assert.That(actualItems.Count, Is.EqualTo(qty));
        }


        [Then(@"the following will be output:")]
        public void ThenTheFollowingWillBeOutput(Table table)
        {
            var expectedItems = table.CreateDynamicSet().ToList();
            var actualItems = ScenarioContext.Current.Get<IList<ReceiptItem>>("Receptitems");
            for (var i = 0; i < expectedItems.Count; i++)
            {
                Assert.That(actualItems[i].Desc, Is.EqualTo(expectedItems[i].Desc));
                Assert.That(actualItems[i].Price, Is.EqualTo(expectedItems[i].Price));
                Assert.That(actualItems[i].Sku, Is.EqualTo(expectedItems[i].Sku));
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
