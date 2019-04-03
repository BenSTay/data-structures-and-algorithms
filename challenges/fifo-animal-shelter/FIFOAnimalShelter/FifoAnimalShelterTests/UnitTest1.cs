using System;
using Xunit;
using FIFOAnimalShelter.Classes;

namespace FifoAnimalShelterTests
{
    public class UnitTest1
    {
        [Fact]
        public void AnimalShelterCanFindDog()
        {
            Dog demi = new Dog() { Name = "Demi" };
            Cat boots = new Cat() { Name = "Boots" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(boots);
            shelter.Enqueue(demi);
            Animal result = shelter.Dequeue("dog");
            Assert.IsType<Dog>(result);
        }

        [Fact]
        public void AnimalShelterCanFindCat()
        {
            Dog demi = new Dog() { Name = "Demi" };
            Cat boots = new Cat() { Name = "Boots" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(demi);
            shelter.Enqueue(boots);
            Animal result = shelter.Dequeue("cat");
            Assert.IsType<Cat>(result);
        }

        [Fact]
        public void AnimalShelterGetsFirstAnimalIfNoPreference()
        {
            Dog demi = new Dog() { Name = "Demi" };
            Cat boots = new Cat() { Name = "Boots" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(boots);
            shelter.Enqueue(demi);
            Animal result = shelter.Dequeue();
            Assert.IsType<Cat>(result);
        }

        [Fact]
        public void AnimalShelterReturnsNullIfNoDogsFound()
        {
            Cat boots = new Cat() { Name = "Boots" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(boots);
            Animal result = shelter.Dequeue("dog");
            Assert.Null(result);
        }

        [Fact]
        public void AnimalShelterReturnsNullIfNoCatsFound()
        {
            Dog demi = new Dog() { Name = "Demi" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(demi);
            Animal result = shelter.Dequeue("cat");
            Assert.Null(result);
        }

        [Fact]
        public void AnimalShelterReturnsNullIfShelterIsEmpty()
        {
            AnimalShelter shelter = new AnimalShelter();
            Animal result = shelter.Dequeue();
            Assert.Null(result);
        }

        [Fact]
        public void AnimalShelterThrowsArgumentExceptionIfPrefIsNotDogOrCat()
        {
            Dog demi = new Dog() { Name = "Demi" };
            Cat boots = new Cat() { Name = "Boots" };
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(boots);
            shelter.Enqueue(demi);
            Assert.Throws<ArgumentException>(() => shelter.Dequeue("mouse"));
        }


    }
}
