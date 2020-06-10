using System;
using Xunit;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Tests
{
    public class UporabnikiTests : IDisposable
    {
        Uporabnik uporabnik;

        public UporabnikiTests()
        {
            uporabnik = new Uporabnik
            {
                Email = "random@random.com",
                Ime = "joze",
                Nickname = "Jozex123"
            };
        }

        public void Dispose()
        {
            uporabnik = null;
        }

        [Fact]
        public void SeSpremeniEmail()
        {
        //Given
        
        //When
        uporabnik.Email = "abc";
        //Then
        Assert.Equal("abc", uporabnik.Email);
        }

        [Fact]
        public void SeSpremeniIme()
        {
        //Given
        
        //When
        uporabnik.Ime = "abc";
        //Then
        Assert.Equal("abc", uporabnik.Ime);
        }

        [Fact]
        public void SeSpremeniNickname()
        {
        //Given
        
        //When
        uporabnik.Nickname = "abc";
        //Then
        Assert.Equal("abc", uporabnik.Nickname);
        }
    }
}