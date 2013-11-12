using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using SimplyMobile.Text;

namespace TextSerializationTests
{
	public static class TestMethods
	{
        public static bool CanSerialize<T>(ITextSerializer serializer, T item, ITextSerializer deserializer)
		{
//			person.Pets.Add (new Dog () { Name = "Shorthaired German Pointer" });
//			person.Pets.Add (new Cat () { Name = "Siamese" });

			var text = serializer.Serialize (item);

			Console.WriteLine(text);

            var obj = deserializer.Deserialize<T>(text);

			Console.WriteLine (obj);

			return obj.Equals(item);
		}

        public static bool CanSerializeEnumerable<T>(ITextSerializer serializer, IEnumerable<T> list, ITextSerializer deserializer)
        {
            var text = serializer.Serialize(list);

            var obj = deserializer.Deserialize<IEnumerable<T>>(text);

            return obj.SequenceEqual(list);
        }

		public static long GetSerializationSpeed(int numberOfIterations, ITextSerializer serializer)
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};

			var stopWatch = new Stopwatch ();
			stopWatch.Start();
			for (var n = 0; n < numberOfIterations; n++)
			{
				serializer.Serialize (person);
			}
			stopWatch.Stop();
			return stopWatch.ElapsedMilliseconds;
		}

		public static long GetDeserializationSpeed(int numberOfIterations, ITextSerializer serializer, ITextSerializer deserializer)
		{
			var person = new Person () 
			{
				Id = 0,
				FirstName = "First",
				LastName = "Last"
			};

			var str = serializer.Serialize (person);

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
                deserializer.Deserialize<Person>(str);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}

        public static long GetSerializationSpeed(int numberOfIterations, ITextSerializer serializer, object o, out string text, ITextSerializer deserializer)
		{
			text = string.Empty;

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
                text = deserializer.Serialize(o);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}

        public static long GetDeserializationSpeed<T>(int numberOfIterations, ITextSerializer serializer, string text, out T deserialized, ITextSerializer deserializer)
		{
			deserialized = default(T);

			var stopWatch = new Stopwatch ();
			stopWatch.Start ();
			for (var n = 0; n < numberOfIterations; n++)
			{
                deserialized = deserializer.Deserialize<T>(text);
			}
			stopWatch.Stop ();
			return stopWatch.ElapsedMilliseconds;
		}
	}
}

