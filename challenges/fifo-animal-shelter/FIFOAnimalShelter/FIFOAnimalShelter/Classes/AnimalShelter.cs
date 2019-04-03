using System;
using System.Collections.Generic;
using System.Text;

namespace FIFOAnimalShelter.Classes
{
    public class AnimalShelter
    {
        public Animal Front { get; set; }
        public Animal Rear { get; set; }

        /// <summary>
        /// Adds an animal to the rear of animal shelter's wait list.
        /// </summary>
        /// <param name="animal">The animal being added to the shelter.</param>
        public void Enqueue(Animal animal)
        {
            if (Front is null)
            {
                Front = animal;
                Rear = animal;
            }

            else
            {
                Rear.Next = animal;
                Rear = animal;
            }
        }

        /// <summary>
        /// Gets the first animal in the animal shelter's wait list that matches a preferred animal type,
        /// or the first animal in the wait list if no preference is given.
        /// </summary>
        /// <param name="pref">The preferred animal type.</param>
        /// <returns>The first matching animal.</returns>
        public Animal Dequeue(string pref = "")
        {
            if (Front is null) return null;
            Animal current = Front;
            pref = pref.ToUpper();

            if (pref == "" || GetType(Front) == pref )
            {
                Front = Front.Next;
                current.Next = null;
                return current;
            }

            if (pref != "DOG" && pref != "CAT")
            {
                throw new ArgumentException("Preference must be \"Cat\", \"Dog\", or none.");
            }

            Animal previous;
            do
            {
                previous = current;
                current = current.Next;
            } while (current != null && GetType(current) != pref);

            if (current != null)
            {
                previous.Next = current.Next;
                current.Next = null;
            }

            return current;
        }

        /// <summary>
        /// Helper method to format the type of an animal as a string.
        /// </summary>
        /// <param name="animal">The animal whose type is being checked.</param>
        /// <returns>The formatted type string.</returns>
        private static string GetType(Animal animal)
        {
            string[] splitTypeString = animal.GetType().ToString().Split('.');
            return splitTypeString[splitTypeString.Length - 1].ToUpper();
        }
    }
}
