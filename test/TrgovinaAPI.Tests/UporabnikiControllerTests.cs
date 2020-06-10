using System;
using Xunit;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TrgovinaAPI.Controllers;
using TrgovinaAPI.Models;

namespace TrgovinaAPI.Tests
{
    public class UporabnikiControllerTests : IDisposable
    {
        DbContextOptionsBuilder<UporabnikiContext> optionsBuilder;
        UporabnikiContext dbContext;

        UporabnikiController controller;

        Uporabnik uporabnik;

        public UporabnikiControllerTests()
        {
            optionsBuilder = new DbContextOptionsBuilder<UporabnikiContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemDB");
            dbContext = new UporabnikiContext(optionsBuilder.Options);

            controller = new UporabnikiController(dbContext);

            //testni uporabnik

            uporabnik = new Uporabnik
            {
                Email = "bolek@lolek.si",
                Ime = "Janez",
                Nickname = "Johny 31"
            };
        }

        public void Dispose()
        {
            optionsBuilder = null;
            foreach(var uporabnik in dbContext.Uporabniki)
            {
                dbContext.Uporabniki.Remove(uporabnik);
            }
            dbContext.SaveChanges();
            dbContext.Dispose();
            controller = null;
            uporabnik = null;
        }

        [Fact]
        public void GetUporabniki_ReturnZeroItems_WhenDBIsEmpty()
        {
        //Given
        
        //When
        var result = controller.GetUporabniki();
        //Then
        Assert.Empty(result.Value);
        }

        [Fact]
        public void GetUporabniki_ReturnOneItems_WhenOneObjectExist()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();
        //When
        var result = controller.GetUporabniki();
        //Then
        Assert.Single(result.Value);
        }

        [Fact]
        public void GetUporabniki_ReturnNItems_WhenNObjectExist()
        {
        //Given
        Uporabnik uporabnik2 = new Uporabnik
        {
            Email = "as@sd.c",
            Ime = "Klemen",
            Nickname = "klemenko"
        };
        
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.Uporabniki.Add(uporabnik2);
        dbContext.SaveChanges();
        //When
        var result = controller.GetUporabniki();
        //Then
        Assert.Equal(2, result.Value.Count());
        }

        [Fact]
        public void GetUporabniki_ReturnCorrect()
        {
        //Given
        
        //When
        var result = controller.GetUporabniki();
        //Then
        Assert.IsType<ActionResult<IEnumerable<Uporabnik>>>(result);
        }

        [Fact]
        public void GetUporabniki_ReturnsNullResult_WhenInvalidId()
        {
        //Given
        
        //When
        var result = controller.GetUporabniki(0);
        //Then
        Assert.Null(result.Value);
        }

        [Fact]
        public void GetUporabniki_Returns404NotFound_WhenInvalidId()
        {
        //Given
        
        //When
        var result = controller.GetUporabniki(0);
        //Then
        Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetUporabniki_ReturnTheCorrectType()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        //When
        var result = controller.GetUporabniki(UporabnikId);
        //Then
        Assert.IsType<ActionResult<Uporabnik>>(result);
        }

        [Fact]
        public void GetUporabniki_ReturnTheCorrectResource()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        //When
        var result = controller.GetUporabniki(UporabnikId);
        //Then
        Assert.Equal(UporabnikId, result.Value.Id);
        }

        [Fact]
        public void PostUporabniki_ObjectCountIncrement_WhenValidObject()
        {
        //Given
        var oldCount = dbContext.Uporabniki.Count();
        //When
        var result = controller.PostUporabniki(uporabnik);
        //Then
        Assert.Equal(oldCount + 1, dbContext.Uporabniki.Count());
        }

        [Fact]
        public void PostUporabniki_Returns201Create_WhenValidObject()
        {
        //Given
        
        //When
        var result = controller.PostUporabniki(uporabnik);
        //Then
        Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public void PutUporabniki_AttributeUpdated_WhenValidObject()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        uporabnik.Nickname = "Mojca";
        //When
        controller.PutUporabniki(UporabnikId, uporabnik);
        var result = dbContext.Uporabniki.Find(UporabnikId);
        //Then
        Assert.Equal(uporabnik.Nickname, result.Nickname);
        }

        [Fact]
        public void PutUporabniki_Return204_WhenValidObject()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        uporabnik.Nickname = "Mojca";
        //When
        var result = controller.PutUporabniki(UporabnikId, uporabnik);
        //Then
        Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public void PutUporabniki_Return400_WhenInvalidObject()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id +1;
        uporabnik.Nickname = "Mojca";
        //When
        var result = controller.PutUporabniki(UporabnikId, uporabnik);
        //Then
        Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void PutUporabniki_AtributeUnchanged_WhenInvalidObject()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        Uporabnik uporabnik2 = new Uporabnik
        {
            Id = uporabnik.Id,
            Email = "as@sd.c",
            Ime = "Klemen",
            Nickname = "klemenko"
        };

        //When
        controller.PutUporabniki(uporabnik.Id + 1, uporabnik2);
        var result = dbContext.Uporabniki.Find(uporabnik.Id);
        //Then
        Assert.Equal(uporabnik.Nickname, result.Nickname);
        }

        [Fact]
        public void DeleteUporabniki_ObjectDecrement_WhenValidObjectId()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        var objCount = dbContext.Uporabniki.Count();
        //When
        var result = controller.DeleteUporabniki(UporabnikId);
        //Then
        Assert.Equal(objCount - 1, dbContext.Uporabniki.Count());
        }

        [Fact]
        public void DeleteUporabniki_Return204OK_WhenValidObjectId()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        //When
        var result = controller.DeleteUporabniki(UporabnikId);
        //Then
        Assert.Null(result.Result);
        }

        [Fact]
        public void DeleteUporabniki_Return404NotFound_WhenValidObjectId()
        {
        //Given
        
        //When
        var result = controller.DeleteUporabniki(-1);
        //Then
        Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void DeleteUporabniki_ObjectCountNotDecrement_WhenValidObjectId()
        {
        //Given
        dbContext.Uporabniki.Add(uporabnik);
        dbContext.SaveChanges();

        var UporabnikId = uporabnik.Id;
        var objCount = dbContext.Uporabniki.Count();
        //When
        var result = controller.DeleteUporabniki(UporabnikId + 1);
        //Then
        Assert.Equal(objCount, dbContext.Uporabniki.Count());
        }
    }
}