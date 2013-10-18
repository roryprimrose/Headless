# Headless

Headless is a .Net framework for executing web acceptance tests without the overhead of running a browser. It is fast, really really fast.

Read the [Wiki](https://github.com/roryprimrose/Headless/wiki) for information about usage.

You can download Headless from [NuGet](http://www.nuget.org/packages/Headless).

Benefits
-
- Fast web acceptance tests
- HTML element resolution
- Forms support
- Hyperlink support
- Manual, page or dynamic programming models
- Location and status code validation
- Extensible

Performance
-
The performance of Headless is fast. It is much faster than [WatiN](http://watin.org/) which is much faster than Microsoft's CUIT. 

Anecdotally, we found that WatiN was up to 10 times faster than CUIT. We have found similar performance gains with Headless over WatiN. In one test suite of 50 tests, WatiN executed the suite in 7:40 minutes while Headless clocked in at 49 seconds. In another test suite of 23 tests, WatiN executed the suite in 4:10 minutes while Headless completed in 19 seconds. An entire enterprise solution has now been converted from WatiN to Headless with the execution time dropping from 1:04hr down to just over 7 minutes.

To be clear, this does not mean that Headless is better than WatiN, just different and faster.

WatiN vs Headless
-
Ideally a good acceptance test suite would leverage both WatiN and Headless. So when should you use WatiN or Headless? Use Headless if you have a test that can be executed with just HTML support (a browser with JavaScript disabled for example). Use WatiN if your test requires JavaScript, CSS or other resources to be processed by an actual browser.

Headless is not fully featured like WatiN because it does not use a browser. It simply simulates browser interactions with HTML in terms of links and forms. This however makes Headless faster than WatiN because it doesn't:

- create a browser for each test
- wait for page resources to be downloaded
- wait for scripts to be executed