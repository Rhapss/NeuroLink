using System;


namespace NeuroLink
{

    class Program
    {
        public class Neuron
        {
            private decimal weight = 0.5m;
            public decimal LastError { get; private set; }
            public decimal Smoothing { get; set; } = 0.00001m;
            public decimal ProcessInputData(decimal input)
            {
                return input * weight;
            }
            public decimal RestoreInputData(decimal output)
            {
                return output / weight;
            }
            public void Train(decimal input, decimal expectedResult)
            {
                var actualResult = input * weight;
                LastError = expectedResult - actualResult;
                var correction = (LastError / actualResult) * Smoothing;
                weight += correction;
            }
        }
        static void Main(string[] args)
        {
            decimal usd = 1;
            decimal uah = 27.48m;

            Neuron neuron = new Neuron();

            int i = 0;
            do
            {
                i++;
                neuron.Train(usd, uah);
                if (i % 100000 == 0)
                {
                    Console.WriteLine($"Итерация: {i}\tОшибка:\t{neuron.LastError}");
                }
            } while (neuron.LastError > neuron.Smoothing || neuron.LastError < -neuron.Smoothing);

            Console.WriteLine("Обучение завершено!");

            Console.WriteLine($"{neuron.ProcessInputData(228)} гривен в {228} долларах");

            Console.WriteLine($"{neuron.RestoreInputData(1451)} долларов в {1451} гривнах");

            Console.WriteLine($"{neuron.ProcessInputData(usd)} гривен в {usd} долларах");
        }
    }
}
