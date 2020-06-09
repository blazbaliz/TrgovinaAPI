using System;
using Xunit;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Tests
{
    public class IzdelkiTests : IDisposable
    {
        Izdelek testniIzdelek;
        
        public IzdelkiTests()
        {
            testniIzdelek = new Izdelek
            {
                ImeIzdelka = "Zoga",
                OpisIzdelka = "kr neki",
                CenaIzdelka = 30,
                Zaloga = 10,
                NaVoljo = true
            };
        }

        public void Dispose()
        {
            testniIzdelek = null;
        }

        [Fact]
        public void SeSpremeniImeIzdelka()
        {
        //Given
           
        //When
            testniIzdelek.ImeIzdelka = "Lolekbolek";
        //Then
            Assert.Equal("Lolekbolek", testniIzdelek.ImeIzdelka);

        }

        [Fact]
        public void SeSpremeniOpisIzdelka()
        {
        //Given
        
        //When
            testniIzdelek.OpisIzdelka = "opisizdelka";
        //Then
            Assert.Equal("opisizdelka", testniIzdelek.OpisIzdelka);
        }

        [Fact]
        public void SeSpremeniCenaIzdelka()
        {
        //Given
        
        //When
            testniIzdelek.CenaIzdelka = 15;
        //Then
            Assert.Equal(15, testniIzdelek.CenaIzdelka);
        }

        [Fact]
        public void SeSpremeniZaloga()
        {
        //Given
        
        //When
            testniIzdelek.Zaloga = 10;
        //Then
            Assert.Equal(10, testniIzdelek.Zaloga);
        }

        [Fact]
        public void SeSpremeniNaVoljo()
        {
        //Given
        
        //When
            testniIzdelek.NaVoljo = false;
        //Then
            Assert.False (testniIzdelek.NaVoljo);
        }
    }
}