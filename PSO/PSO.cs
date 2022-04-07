using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSO
{


    class PSO
    {
        int N;
        double[][] V;
        public double[][] X;
        double[] loss;

        int Index;
        int opIndex;

        static int ini = 40;
        public double lossBest = ini * ini;
        public double localBest;
        double[] pBest;
        double[] gBest;

        public int Smax = ini;
        public int Smin = -ini;
        public double Vmax = Math.Sqrt(ini);
        public double Vmin = -Math.Sqrt(ini);
        double Xmax = ini;
        double Xmin = -ini;

        double w = 0.5;
        double wmax = 1; double wmin = 0.1;
        double c1 = 2;
        double c2 = 2;

        public int iteration = 0;
        int itmax = 50;// ini * ini;

        Random r;

        public PSO(int N)
        {
            r = new Random();

            this.N = N;
            V = new double[N][];
            X = new double[N][];
            pBest = new double[2];
            gBest = new double[2];
            for (int i = 0; i < N; i++)
            {
                V[i] = new double[2];
                X[i] = new double[2];
            }
            loss = new double[N];
        }

        public void initialize()
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    X[i][j] = r.Next(Smin, Smax);
                    V[i][j] = r.Next((int)Vmin, (int)Smax);
                }
            }
            getLoss();
            gBest[0] = X[Index][0]; gBest[1] = X[Index][1];
        }

        public double f(double[] input)
        {
            return -20 * Math.Exp(-0.2 * Math.Sqrt(0.5 * (input[0] * input[0] + input[1] * input[1])))
                - Math.Exp(1) * 0.5 * (Math.Cos(2 * Math.PI * input[0]) + Math.Cos(2 * Math.PI * input[1]))
                + 20 + Math.Exp(1);
        }

        public void getLoss()
        {
            for (int i = 0; i < N; i++) loss[i] = f(X[i]);

            localBest = loss.Min();
            Index = Array.IndexOf(loss, localBest);
            pBest[0] = X[Index][0]; pBest[1] = X[Index][1];

            if (localBest < lossBest)
            {
                lossBest = localBest;
                gBest[0] = pBest[0];
                gBest[1] = pBest[1];
            }
        }

        public void getV()
        {
            //w = w - iteration / itmax;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    double r1 = r.NextDouble();
                    double r2 = r.NextDouble();
                    V[i][j] = w * V[i][j]
                                        + c1 * r1 * (pBest[j] - X[i][j])
                                        + c2 * r2 * (gBest[j] - X[i][j]);

                    if (V[i][j] < Vmin) V[i][j] = Vmin;
                    else if (V[i][j] > Vmax) V[i][j] = Vmax;

                    if (X[i][j] < Xmin) X[i][j] = Xmin;
                    else if (X[i][j] > Xmax) X[i][j] = Xmax;
                }
            }
        }

        public void getX()
        {
            for (int i = 0; i < N; i++)
            {
                X[i][0] = X[i][0] + V[i][0];
                X[i][1] = X[i][1] + V[i][1];
            }
        }

        public void optimize()
        {
            getV();
            getX();
            getLoss();
            iteration++;
        }

    }
}
