using System;
using Xunit;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Tests
{
    public class NarocilaTests : IDisposable
    {
        Narocilo narocilo;

        public NarocilaTests()
        {
            narocilo = new Narocilo
            {
               UporabnikId = 1,
               IzdelekId = 1,
               Kolicina = 1,
               Status = "odprto"
            };
        }

        public void Dispose()
        {
            narocilo = null;
        }

        [Fact]
        public void SeSpremeniUporabnikId()
        {
        //Given
        
        //When
        narocilo.UporabnikId = 2;
        //Then
        Assert.Equal(2, narocilo.UporabnikId);
        }

        [Fact]
        public void SeSpremeniIzdelekId()
        {
        //Given
        
        //When
        narocilo.IzdelekId = 3;
        //Then
        Assert.Equal(3, narocilo.IzdelekId);
        }

        [Fact]
        public void SeSpremeniKolicina()
        {
        //Given
        
        //When
        narocilo.Kolicina = 2;
        //Then
        Assert.Equal(2, narocilo.Kolicina);
        }

        [Fact]
        public void SeSpremeniStatus()
        {
        //Given
        
        //When
        narocilo.Status = "Zakljuceno";
        //Then
        Assert.Equal("Zakljuceno", narocilo.Status);
        }
    }
}