<h1 align="right">
  <img src="https://github.com/pferreirafabricio/classify-error-messages/assets/42717522/a000075f-1d59-422c-b920-1b2d9c785dce" width="200px" align="left" />
  Classify Errors Messages
</h1>

<p align="right">
  Trying to achieve this process when the user encounters errors: 🤬 -> 🤔 -> 😮 -> 🤩. Studying the best approaches to classifying errors
  <br><br>
  <!-- License -->
  <a>
    <img alt="license url" src="https://img.shields.io/badge/license%20-MIT-1C1E26?style=for-the-badge&labelColor=2E2E2E&color=42D0FC">
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

Error handling is a critical part of software development, but programmers often approach it in an ad-hoc manner. Since human error is unavoidable, and errors can occur whenever people or external systems are involved, it's essential to handle them thoughtfully. Effective error handling is as important as the core business logic, ensuring your software continues to function despite unexpected issues.

### Types of Errors

#### 1. Exceptional Errors vs. Failures

- Exceptional Errors: These are unexpected but possible events. You safeguard against them even if they rarely occur. Examples include:
  - Running out of memory
  - Invalid JSON format
  - Missing database object

  Exceptional errors should be handled gracefully but are rare enough that frequent occurrence would indicate a design flaw. If errors are expected regularly, they aren't truly exceptional.

- Failures: Failures occur when an operation cannot proceed, and they happen more predictably. Examples include:
  - Incorrect passwords
  - CDN downtime
  - Missing device permissions
  - No network connection

  Failures are handled in various ways, such as with error codes, strings, or conditional logic, depending on the situation.

#### 2. Internal vs. External Errors

- Internal Errors: These are caused by flaws in the system's design or logic. Examples include:
  - Incorrect use of operators
  - Misconstructed regular expressions
  - Poorly written tests

  Internal errors should be prevented, not handled at runtime. Unit tests help catch these, and error handling for internal issues should be minimal because the focus should be on prevention.

- External Errors: These originate from outside the system, such as from users, third-party services, or dependencies. External errors are inevitable and should be handled with more complexity, as you cannot prevent them. Integration tests are suited for these errors since they involve interactions with the external environment.

#### Why Differentiating Matters

- Internal Errors: Easier to prevent but harder to handle. Unit testing and validation are key.

- External Errors: Easier to handle but harder to prevent. These require robust error-handling strategies.

### Accuracy and Precision in Error Messaging

Effective error messaging must be precise and helpful, especially when errors affect the user. The precision of error messages can vary:

- General: "These credentials are invalid."
- Specific: "The password is incorrect."
- Detailed: "The given password is two characters off from the expected password."

The goal is to provide users with enough information to understand what went wrong and how to fix it.

### Fit for Use

The usefulness of error data depends on how it's gathered and used. For failures, error messages must provide enough context for the user to recover from the error. In the case of exceptional errors, messages should enable system recovery, and the error handling strategy should be flexible depending on the programming environment.

### Shaping Error Data

Every error should have a well-structured class, error code, and name. Key data fields include:

- Which operation failed
- Invalid inputs
- Input values
- Timestamps
- User-readable messages

Logging error data immediately is important, but only necessary information should be passed back to the user.

### Balancing Error Complexity

Error handling must strike a balance between simplicity and necessary complexity:

- Simple Approach: Boolean values or basic error codes that signal the occurrence of an error.
- Complex Approach: Defining classes for every error, which can add unnecessary overhead.

Error handling should be complex enough to support error recovery and analytics without overburdening the system.

### Error Recovery and Propagation

Errors should be logged as soon as they are detected. Recovery strategies include either fixing the issue (like auto-correcting or showing a user-friendly error message) or passing the error along. For instance:

- Internal Error: Inform the user about a failure beyond their control and work on future prevention (e.g., bug reporting).
- External Error: Guide the user on how to resolve the issue or suggest that the problem is outside their influence.

### Error Categories

#### 1. Background Errors

Automatically handled, such as a connection timeout with an auto-reconnect feature.

#### 2. Auto-fixable Errors

Errors that can be corrected without user involvement but should still inform the user of the action taken. Example: An out-of-stock item is removed from a cart.

#### 3. Preventable Errors

Errors that can be avoided through design improvements. Examples include:

- Disabling buttons in insufficient wallet balance scenarios.
- Providing password setup instructions to prevent validation issues.

#### 4. Fixable Errors

These are inevitable but can be resolved by providing clear, user-friendly error messages. Avoid ambiguous error codes that require users to consult external resources.

#### 5. Non-fixable Errors

For errors that cannot be fixed by the user, error reporting tools should be integrated to allow developers to address them efficiently.

#### 6. Design F*ck-ups

Critical UX failures, such as app crashes or blank screens, that abruptly terminate the user experience.

### Best Practices for Error Messaging

#### Primary goals

- Make errors easy to discover and map them to where they occur.
- Prevent errors wherever possible.
- Keep error messages short, clear, and free of jargon.
- Avoid blaming the user and focus on offering clear steps for resolution.

#### Secondary goals

- Use real-time validation and system helpers like autocomplete.
- Automatically fix errors when possible.
- Group and sequence data logically.
- Explain unfamiliar concepts clearly.

### Exceptions vs. Flow Control

Exceptions should only be used for exceptional situations, not for normal flow control, as this complicates the code and makes it harder to manage. Instead, handle known errors explicitly and reserve exceptions for cases where recovery is impossible or unknown.

### Conclusion

Prevent internal errors; handle external errors.
Keep error handling as simple as possible but complex enough for effective recovery and analysis.
Always prioritize user-friendly error messages and recovery paths.

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
