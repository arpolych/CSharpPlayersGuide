using System.Numerics;

namespace PracticeField
{
    public class Examples_p353
    {
        public static void Main()
        {
            Console.WriteLine("Going for example-app_2...");
            ExampleApplication_2().Wait();

            Console.WriteLine("Going for example-app_3...");
            ExampleApplication_3()
                .Wait()
                ;
        }

        public static void ExampleApplication_1()
        {
            var example = new Examples_p353();
            Task<int> additionTask = example.AddOnEuropaVersion_3(2, 3);

            // additionTask.Wait(); -- redundant when Result is present afterwards...

            int result = additionTask.Result;

            Console.WriteLine("Example-app_1 result: " + result);
            Console.WriteLine("Done with example-app_1...");
        }

        /*
         * This Task application example is run asynchronously, thus, if the
         * results are not awaited the calling thread will continue on...
         */
        public static Task ExampleApplication_2()
        {
            var example = new Examples_p353();
            Task<int> additionTask = example.AddOnEuropaVersion_3(2, 3);
            Task addAndDisplay = additionTask.ContinueWith(t =>
            {
                Console.WriteLine($"t: {t}");
                Console.WriteLine("Example-app_2 result: " + t.Result);
            });

            Console.WriteLine("Done with example-app_2...");
            return addAndDisplay;
        }

        public static Task ExampleApplication_3()
        {
            var example = new Examples_p353();
            Task<int> additionTask = example.AddOnEuropaVersion_3(2, 3);
            Task<double> moreMath = additionTask.ContinueWith<double>(t => t.Result * 2);
            Task displayMath = moreMath.ContinueWith(value => Console.WriteLine($"Example-app_3 result: {moreMath.Result}"));

            Console.WriteLine("Done with example-app_3...");

            return displayMath;
        }

        public Task<int> AddOnEuropaVersion_1(int a, int b)
        {
            Thread.Sleep(3_000);

            int result = a + b;

            return Task.FromResult(result);
        }

        public Task<int> AddOnEuropaVersion_2(int a, int b)
        {
            Task<int> task = new(() =>
            {
                Thread.Sleep(3_000);

                return a + b;
            });

            task.Start();

            return task;
        }

        public Task<int> AddOnEuropaVersion_3(int a, int b)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(3_000);

                return a + b;
            });
        }
    }
}
