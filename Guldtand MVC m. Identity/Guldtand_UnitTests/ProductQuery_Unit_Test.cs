﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuldtandMVC_Identity;
using GuldtandMVC_Identity.Data;
using GuldtandMVC_Identity.Data.Queries;
using GuldtandMVC_Identity.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Guldtand_UnitTests
{
    class ProductQueryUnitTest
    {
        private ProductQuery _uut;
        private Prj4databaseContext _context;
        [SetUp]
        public void SetUp()
        {
            string ConnectionString = "Data Source=prj4-server.database.windows.net;Initial Catalog=prj4-database;User ID=maloudt;Password=Mldt1160";

            SqlConnection conn = new SqlConnection(ConnectionString);
            conn.Open();

            var options = new DbContextOptionsBuilder<Prj4databaseContext>()
                .UseSqlServer(conn).Options;

            _context = new Prj4databaseContext(options);
            _uut = new ProductQuery {NumberOfProducts = 10000};
        }

        [Test]
        public async Task ExecuteQuery_ValidDateSet20501201_OnlyProductsFromDateReturned()
        {
            _uut.ValidToDate = "2050";
            var products = await _uut.Execute(_context);
            foreach (var product in products)
            {
                Assert.That(product.ValidTo.Year, Is.EqualTo(2050));
            }
        }

        [Test]
        public async Task ExecuteQuery_ValidDateSet20191204_OnlyProductsFromDateReturned()
        {
            _uut.ValidToDate = "2019-12-04";
            var products = await _uut.Execute(_context);
            foreach (var product in products)
            {
                Assert.That(product.ValidTo.Year, Is.EqualTo(2019));
                Assert.That(product.ValidTo.Month, Is.EqualTo(12));
                Assert.That(product.ValidTo.Day, Is.EqualTo(04));
            }
        }

        [Test]
        public async Task ExecuteQuery_BannedFoetex_NoProductsFromTheseChainsFound()
        {
            _uut.Stores = new string[] { "føtex" };
            _uut.LoadRetailChain = true;

            var products = await _uut.Execute(_context);
            foreach (var product in products)
            {
                foreach (var store in _uut.Stores)
                {
                    Assert.That(product.RetailChain.Name, Is.Not.EqualTo(store));
                }
            }
        }

        [Test]
        public async Task ExecuteQuery_BannedNettoRemaBilkaAldi_NoProductsFromTheseChainsFound()
        {
            _uut.Stores = new string[] {"Netto", "rema", "bilka", "aldi"};
            _uut.LoadRetailChain = true;

            var products = await _uut.Execute(_context);
            foreach (var product in products)
            {
                foreach (var store in _uut.Stores)
                {
                  Assert.That(product.RetailChain.Name, Is.Not.EqualTo(store));
                }
            }
        }
    }
}
