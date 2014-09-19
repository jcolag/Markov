// <copyright file="Markov.cs" company="John Colagioia">
//     John.Colagioia.net. Licensed under the GPLv3
// </copyright>
// <author>John Colagioia</author>
namespace MarkovProcess
{
        using System.Collections.Generic;

        /// <summary>
        /// A Markov process.
        /// </summary>
        /// <typeparam name="T">Type of elements to monitor and generate.</typeparam>
        public class Markov<T>
        {
                /// <summary>
                /// The current state for training.
                /// </summary>
                private Queue<T> trainingState;

                /// <summary>
                /// The current state for generating content.
                /// </summary>
                private Queue<T> generatingState;

                /// <summary>
                /// The states.
                /// </summary>
                private Dictionary<string, Distribution<T>> states = new Dictionary<string, Distribution<T>>();

                /// <summary>
                /// The maximum queue length.
                /// </summary>
                private int length;

                /// <summary>
                /// The random number generator.
                /// </summary>
                private System.Random rand = new System.Random();

                /// <summary>
                /// Initializes a new instance of the <see cref="MarkovProcess.Markov{T}"/> class.
                /// </summary>
                /// <param name="order">Order of the process.</param>
                public Markov(int order)
                {
                        this.length = order;
                        this.trainingState = new Queue<T>(order);
                        this.generatingState = new Queue<T>(order);
                }

                /// <summary>
                /// Adds the next element.
                /// </summary>
                /// <param name="next">The element to add.</param>
                public void AddNext(T next)
                {
                        var key = this.InternalKey(this.trainingState);

                        if (!this.states.ContainsKey(key))
                        {
                                this.states.Add(key, new Distribution<T>());
                        }

                        this.states[key].Add(next);

                        this.trainingState.Enqueue(next);
                        if (this.trainingState.Count > this.length)
                        {
                                this.trainingState.Dequeue();
                        }
                }

                /// <summary>
                /// Clears the training queue.
                /// </summary>
                public void ClearTraining()
                {
                        while (this.trainingState.Count > 0)
                        {
                                this.trainingState.Dequeue();
                        }
                }

                /// <summary>
                /// Generate the next element for this sequence.
                /// </summary>
                /// <returns>The element.</returns>
                public T Generate()
                {
                        var key = this.InternalKey(this.generatingState);

                        if (!this.states.ContainsKey(key))
                        {
                                while (this.generatingState.Count > 0)
                                {
                                        this.generatingState.Dequeue();
                                }

                                return default(T);
                        }

                        Distribution<T> dist = this.states[key];
                        int index = this.rand.Next(dist.TotalCount);
                        T value = dist.AtIndex(index);

                        this.generatingState.Enqueue(value);
                        if (this.generatingState.Count > this.length)
                        {
                                this.generatingState.Dequeue();
                        }

                        return value;
                }

                /// <summary>
                /// Generates an internal key.
                /// </summary>
                /// <returns>The key.</returns>
                /// <param name="state">The queue to use.</param>
                private string InternalKey(Queue<T> state)
                {
                        T[] keys = state.ToArray();
                        string key = string.Empty;
                        foreach (T k in keys)
                        {
                                key += k.ToString() + " ";
                        }

                        key = key.Trim();
                        return key;
                }
        }
}