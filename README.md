# UntitledGame

This game is still early in its development, but a thousand or so lines of code already exist. Of particular interest might be Element.cs, which emulates a "string" enum by having only a private constructors. There are 5 "elements" in this game that can be combined into 26 others, and that class implements the logic for combining those elements, whether by the "+" operator or by passing an array to Transmute.

The way that cards are passed around from the deck, to the hand, to the transmutor, and back again, is currently being redesigned to minimize casting between child classes of Card. The proposed design is being sketched out in UML, and will be posted once it has been finalized.
