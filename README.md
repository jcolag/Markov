Markov
======

This is generic code for a Markov process.

Every once in a while, I want random but reasonable-looking content.  The simplest tool for such a thing is a [Markov process](https://en.wikipedia.org/wiki/Markov_process).  We learn the probability distribution of a text, then generate new text that conforms to that distribution.  Very handy.

Generally, I've used this for random names to plug into software tests, but I don't always want a letter-based process.  So, this version uses a type parameter.  If you want to generate characters, words, heights, instructions, or stacks of processes, this should work, as long as the harness program can specify the data.

Structure
---------

The solution includes two projects.

 - `Markov` itself is just a main program that pushes a text file, character by character, into the process object.

 - `MarkovProcess` is where the real magic happens.  Include this in any project where you want to use it.

Operation
---------

Unlike a standard implementation might choose, _Markov_ doesn't have any idea of a "training mode."  Separate states are maintained for learning and for generation, so both can happen together, if desired.

The API for the `MarkovProcess` class includes:

 - `AddNext(`_element_`)` learns the next element in the sequence.

 - `ClearTraining()` resets the training queue.  It does _not_ reset the training data.  It only creates a new starting state for the generation.

 - `Generate()` produces the next element in the output sequence that could plausibly have been part of the input at this point.

