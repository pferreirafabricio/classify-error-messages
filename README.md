<h1 align="right">
  <img src="" width="170px" align="left" />
  Classify Errors Messages
</h1>

<p align="right">
  Trying to achieve this process when the user encounters errors: 🤬 -> 🤔 -> 😮 -> 🤩. Studying the best approaches to classifying errors
  <br><br>
  <!-- License -->
  <a>
    <img alt="license url" src="https://img.shields.io/badge/license%20-MIT-1C1E26?style=for-the-badge&labelColor=2E2E2E&color=F03A17">
  </a>
</p>

<br>
<br>

## Summary

This project was organized in 3 parts:

- Concepts
- Strategies to error handling (checkout the [handling-errors](https://github.com/pferreirafabricio/handling-errors) repository)
- Error messages categorization

> [!NOTE]
> This project is part of a series of studies about error handling and classification. Check out the other projects:
>
> - [handling-errors](https://github.com/pferreirafabricio/handling-errors)
> - [simple-railway](https://github.com/pferreirafabricio/simple-railway)

## Concepts

`it seems like programmers (myself included) opt to handle errors totally ad-hoc`

People are involved every step of the way, and human error is completely unavoidable

If involves people, then we could have errors
    - like not paying the bill for a third-party service
    -  I’m talking about anything that could prevent your software from accomplishing what it’s intended to do. That means how you handle errors is just as important as your main business logic.

### Exceptional Errors vs. Failures

To my mind, an exceptional error is something that you don’t really expect to happen but that you safeguard against just in case. Here are a few potential exceptional errors:

The application runs out of memory.
An id has no corresponding database object.
A supposedly-JSON string is not in JSON format.

- graceful way to recover
- That’s why they’re only potential exceptions; if you know they’ll occur frequently, they aren’t exceptional for your system

A failure means that an operation can’t continue for some reason.

Failures include:

A user enters their password incorrectly.
The app can’t download an image because the CDN is down.
A user can’t access a feature because a mobile app doesn’t have the right device permissions.
The app has no network connection.

While exceptions are often handled in programs as specific types (such as an Exception subclass), failures can be represented in many other ways — strings, error codes, conditional statements, etc

### Internal vs. External Errors

Internal errors are caused by mistakes in a program’s design or implementation.

It’s impossible to handle logic-sourced errors at runtime

examples:

Misconstructing a regular expression
Using the wrong operator (for example, using ++ as a prefix rather than a postfix, or vice versa)
Writing a test that doesn’t fail when it’s supposed to

External Errors
These are errors caused by clients and dependencies. (libraries, APIs, people, bots)

In fact, peripheral errors are really the only type of error that it makes sense to handle with any complexity at all. That’s because non-peripheral errors are, by definition, under your control; you should “handle” them by preventing them via tests, validations, etc.

Why Does the Difference Matter?
Internal errors are easier to prevent than to handle.

Unit tests should focus on weeding out internal errors because you’re in total control of inputs and outputs.
Internal errors should face as little error handling as possible because it’s far better to just prevent them with exploratory testing.
You typically won’t write much code for handling internal errors because if you can detect them, you might as well prevent them altogether by fixing your code.
External errors are easier to handle than to prevent.

External errors can’t be prevented because you simply are not in control of them. You may have some hand in designing an external API or in giving your users easy-to-follow instructions, but it’s only inside of your own program that you have real control.
Integration tests are well suited for testing external errors against your application.
External errors should face plenty of error handling because you have no choice other than to let your application break!

#### Accuracy and Precision

Precision:

- “These credentials are invalid.”
- “The password is incorrect.”
- “The given password is two characters off from the expected password.”

#### Fit for Its Intended Use

Usefulness also depends on the reason you’re gathering error data. The most important use is figuring out how you can recover gracefully from the error.

For a failure, that means you need enough information to distinguish the error state from the “good” state, as well as from other error states. The consumer might also need information about why the failure happened (e.g., invalid fields).
For an exceptional error, you just need enough information for a consumer to recognize and recover from the error. The way this pattern matching happens will depend on your system. Object-oriented languages typically use class inheritance, whereas a language with a structural type system may use discriminated unions.

## Shaping Error Data

always error class, error code, and name

### Data Fields

which operation failed, which inputs were invalid, which inputs were given in the first place, the IDs of relevant database objects, the timestamp, user-readable error messages

dump as much data as you can immediately

Log the timestamp, inputs, etc. as soon as the error is detected, and only pass back the information that the consumer needs.

### Complexity

On the simple side, you have Boolean values — either something is an error or it is not

Integer error codes can be just as complex as strings because they have no scalar value, i.e. HTTP 403 is not “bigger” than HTTP 400

When using strings to represent errors, you should treat them the same as symbols. (Symbols are values that are equal to themselves and nothing else, and they can’t be compared any other way.) Note that your error data can include non-symbolic strings, but only for carrying extra information (such as messages to the user), not for uniquely identifying the error.

---

- Complexity Is a Balancing Act

integer code, makes development very difficult because you have to keep track of what Error #35 and #73 and #2683 are inside the code

On the other extreme, you could represent every single error as a class and return an EmailRegexDidNotMatch object when validating email addresses. But compared to returning false, that’s a lot of unnecessary overhead and maintenance.

Your error data should be just complex enough to accomplish the goals we discussed earlier:

Recovering from the error
Recording the right amount of information for forensics and analytics

---

That doesn’t mean you need to implement handling in every single function, but you should be very intentional about including or excluding it.

You can always pass error data between functions.

If possible, turn the error into a domain type. Represent the error as some data structure that is defined by your application, not another library or application

The place where you detect an error is where you should log it.

If the error is exceptional, you might also want to report it to a third-party service like Sentry

Either “recover” from the error or pass it along

"recover" = rendering a 404 page for a client. An example of the latter is rendering errors on a web form while retaining the form’s inputs.

You may also want to log how the recovery will happen (e.g., “undoing x, y, and z” or “retrying…”)

Generally speaking, recovering from internal errors means letting the user know that something out of their control failed and then doing something to make the error less likely in the future (like reporting a bug). Recovering from external errors means telling the user what went wrong and how they can fix it, or that something failed that you may or may not be able to fix.

---

And yet, errors are often the last thing to get attention during design and tend to be treated like the red-headed stepchild. Or worse yet, they are defined and implemented solely by developers — people who are as far removed as you can get from the end-user regarding error understanding.

0. Background errors

- A good example might be a connection timeout with automatic reconnection.

1. Auto-fixable errors

- An example of an auto-fixable issue would be an item left in a shopping cart that’s no longer in stock. It must be removed from the cart to process the order properly, but it shouldn’t just disappear without any information, confusing the user as to why it’s gone.

2. Preventable errors

Preventable errors are fixable errors in nature, but with an added component that enables us to create safeguards in the system that will minimise the number of times, those errors might occur or eliminate them outright from happening.

Now, another cop-out is that preventable errors might not lead to errors at all. With the right setup, we can design components that are error-proof.

Examples

2.1. Components that can limit user interaction to such a degree that an error is not possible
    - disabling a “next” button on a transaction flow if funds in the wallet are insufficient to complete said transaction
2.2. Components we can only limit partially, or can’t limit at all, but we can include “helper” elements
    - Password setup with clear instructions on how that password must be formatted to be validated by the system (one large character, one number, one special character, no spaces, etc)

3. Fixable errors

- Error will happen in certain circumstances and the only thing we can do is to help our users fix them with as little friction as possible.
- so many products and systems have poorly constructed error messages with no real context or course of action for the user

Alternatively, which I think is an equally poor practice, you’ll get an error code, so that you can go to a separate website and check it there. Who exactly benefits from that? It certainly isn’t the user. If there’s a specific reason for the error and a potential way of “repairing” it, why not just display that?

The structure is quite simple and applies in most circumstances — you tell the users what happened, why it happened and what they can do to fix it.

Besides, even if they don’t understand the cause, you still owe them to provide an easy-to-follow course of action to resolve the issue. Asking them to take out their phone and scan a QR code that’s displayed for 2.8 seconds on the screen is not it. (That’s on you Microsoft).

4. Non-fixable errors

What you can do however (or rather, not you, designers, but you, as a team) is to implement proper error reporting tools into your solution. That will enable a quick and reliable way of catching those errors and introducing fixes.

5. Design f*ck-ups

everything from UX paths that terminate abruptly

Examples:

- a blank screen in an app, or a crash in a streaming service that came out of nowhere with no ability to restart said service

---

Primary

Make errors easily discoverable and map them to where they occur.
Prevent errors wherever possible.
Keep error messages short and precise; avoid ambiguity. Use human language and avoid jargon. Describe how to resolve the issue.
Don’t blame the user and use appropriate language.
Don’t provide unnecessary options.
Secondary

Use real-time validation whenever possible.
Employ system helpers like auto-filling, autocomplete, etc.
Autofix errors when/if possible.
Use logical grouping or sequencing of data types.
Explain unfamiliar concepts.

---

6. What to do when you're not sure if you're dealing with an error?

In case you're not sure if a particular action in the app is a bug, it's a good idea to check the documentation and consult the issue with a more experienced team member.

---

One school of thought suggests using exceptions for flow control. This is not a good approach because it makes the code harder to reason about. The caller must know the implementation details and which exceptions to handle.

Exceptions are for exceptional situations.

Using exceptions for flow control is an approach to implement the fail-fast principle.

As soon as you encounter an error in the code, you throw an exception — effectively terminating the method, and making the caller responsible for handling the exception.

The problem is the caller must know which exceptions to handle. And this isn't obvious from the method signature alone.

Since you already expect potential errors, why not make it explicit?

You can group all application errors into two groups:

- Errors you know how to handle
- Errors you don't know how to handle

Exceptions are an excellent solution for the errors you don't know how to handle. And you should catch and handle them at the lowest level possible.

---

Don't use Result if:

- If you need the exception stack trace
- If you don't plan to handle the failure
- If you care about performance (allocations)
- If you can't recover from a failure - it's simpler to throw an exception

## 🧱 This project was built with

- [.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0)

## 🏄‍♂️ Quick Start

 1. Clone this repository `git clone https://github.com/pferreirafabricio/classify-error-messages.git`
 2. Enter in the project's folder: `cd classify-error-messages`
 3. Enter in the API's project: `cd ClassifyErrorMessages`
 4. Run the API: `dotnet watch run`
 5. Finally open the Swagger client: `http://localhost:5003/swagger/index.html`

## 📚 References

- [The Error Handbook, Part 1 – Two Ways to Categorize Errors](https://spin.atomicobject.com/categorize-software-errors/)
- [The Error Handbook, Part 2 – How to Shape and Represent Your Error Data](https://spin.atomicobject.com/capturing-representing-errors/)
- [The Error Handbook, Part 3 – How to Handle Your Errors](https://spin.atomicobject.com/error-handling-process/)
- [Categorising Errors-and how to handle them](https://bootcamp.uxdesign.cc/oops-something-went-wrong-7db5aaab0b57)
- [Railway oriented programming - A recipe for a functional app, part 2](https://fsharpforfunandprofit.com/posts/recipe-part2/)
- [Functional Error Handling in .NET With the Result Pattern](https://www.milanjovanovic.tech/blog/functional-error-handling-in-dotnet-with-the-result-pattern)
- [Problem Details for HTTP APIs - RFC 7807](https://datatracker.ietf.org/doc/html/rfc7807)
- [Best practices for reporting bugs in web projects (and much more)](https://career.comarch.com/blog/best-practices-for-reporting-bugs-in-web-projects-and-much-more/)
