using System;
using FIFOAnimalShelter.Classes;

namespace FIFOAnimalShelter
{
    class Program
    {
        static void Main(string[] args)
        {
            Dog dog = new Dog();
            AnimalShelter shelter = new AnimalShelter();
            shelter.Enqueue(dog);
            Animal animal = shelter.Dequeue("cat");
        }
    }
}
