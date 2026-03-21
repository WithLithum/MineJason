# Contributing Guide

Thank you for investing your time in contributing to our project!

In this guide you will get an overview of the contribution workflow from reporting issues to contributing code by forking and creating Pull Requests.

Use the table of contents icon on the top left corner of this document to get to a specific section of this guide quickly.

## Reporting Issues

### Before reporting

The Issues section is not for asking questions about how to use MineJason.

Please search for existing _open_ issues that describes the issue you want to report. Please do not search for the whole issue description but rather look for keywords. Duplicate issues will be closed and linked to the currently tracked issue for the problem/request.

If an issue was previously reported and resolved, but it appeared again, please make a new report rather than reopening the previous one.

### Languages

It is best that you would use the relevant issue template for the type of the issue you are reporting. You should use proper English with _no slangs_. Using any other language is not recommended and issues made in languages that maintainers cannot understand will be closed.

If you have trouble writing standard English, you may enrol the help of a machine translator. However, beware that they sometimes *do not understand technical concepts* and tends to carry the jargons and different writing styles of the source language as-is, which may make the result hard to understand. We recommend that you use a reputable machine translator, such as Bing or Google.

If you plan to work on the issue, please specify in the description. However, if you specify you plan to work on it but no Pull Requests have been submitted by you for a reasonably long period of time, we may end up choosing to open the issue for other contributions.

Below are the specific guidelines for each type of issue reports.

### Bug reports

A bug report is a report on something that isn't working or isn't working as expected.

Please describe the following things in your bug report as detailed as you can describe it:

- exact expected behaviour (what _should_ happened)
- actual behaviour (what actually _have_ happened)
- steps to reproduce the issue

A proof of concept code would be the most helpful if it is an API behaviour issue.

Please make one issue for each bug, rather than reporting multiple bugs in one report.

### Feature requests

When submitting feature requests:

- Describe what is the feature you want, and what does it do?
- What is the intended use case of the feature?
- Are there any workarounds for the lacking of the feature?

The feature request will be evaluated and processed accordingly. However, do note that if the intended use case of the feature is outside of the general scope of the project, then the feature request will be rejected.

Please make an issue for each feature rather than requesting several features in a single issue.

### Enhancement requests

When submitting enhancement requests, describe:

- What exactly is the enhancement you want?
- Why is the enhancement needed?

Enhancement requests should not generally involve making breaking changes, unless its benefits justify doing so.

### After reporting

Please note that all issues will be prioritised based on its urgency and importance.

Usually, critical bugs (like serious regressions, stuff broken by Mojang updates, unexpected exceptions or major API unintended breaking change) will be fixed first.

## Submitting Code Contributions

The procedure for submitting code contributions is straight-forward,
but there are some considerations when submitting new features to the MineJason project.

By submitting code contributions you agree to the [Developer Certificate of Origin](#developer-certificate-of-origin). It is not strictly necessary to add a `Signed-off-by` comment in your commits, however it is a best-have as it tells us that you explicitly agrees to the Developer Certificate of Origin.

Your IDE and/or Git client should be able to do it for you automatically; consult the documentation of your IDE or Git client. For command-line commits, add `-s` or `--signoff` in the `commit` command (or any other command in question).

## Setting up Development Environment

See the [setup guide](SETUP.md).

## Intended Use Case

The intended use case is to aid in programmatic creation of Raw JSON text format serialised text components to be used with the vanilla Minecraft: Java Edition software.

Specifically, these use cases are _not_ supported:

- Use for Bedrock Edition
- Implementing anything that is outside Minecraft

## DO and DO NOTS

Please do:

- **DO** follow the [style guide](docs/STYLEGUIDE.md).
- **DO** give priority to the current style of the project or file you're changing even if it diverges from the general guidelines.
- **DO** include tests when adding new features. When fixing bugs, start with adding a test that highlights how the current behaviour is broken.
- **DO** keep the discussions focused. When a new or related topic comes up it's often better to create new issue than to side track the discussion.
- **DO** clearly state on an issue that you are going to take on implementing it.

Please do not:

- **DO NOT** make PRs for style changes.
- **DO NOT** surprise us with big merge requests. Instead, file an issue and start a discussion so we can agree on a direction before you invest a large amount of time.
- **DO NOT** submit PRs that alter licensing related files or headers. If you believe there's a problem with them, file an issue and we'll be happy to discuss it.

### Breaking Changes

Contributions with breaking changes are accepted, but:

- It will only be included for the next major version (in `0.x` this means immediate inclusion)
- It will be subject to review on API
- It requires proposal issue and approval prior to Pull Request being created.

### Submitting Pull Requests

Prior to submitting pull requests for your first time as a beginner, please take you time to understand what a pull request is and how to do it correctly.

Please don't surprise us with big pull requests that makes significant changes. Instead, file an issue and start a discussion so we can agree on a direction before you invest a large amount of time.

## Developer Certificate of Origin

The [Developer Certificate of Origin](https://developercertificate.org/) (DCO) is a document which when you agree, you promise to us that:

- You are the original author of the code you contribute, or you have permission to do so

- You license the contributed code to us under the same licence as the project you are contributing to

By making a pull request, you agrees to the DCO. If you do not agree to it, do not send a pull request. You may add a `Signed-off-by` field to your Git commit description to explicitly agree to it, but it is not strictly necessary.

```plain
Developer Certificate of Origin
Version 1.1

Copyright (C) 2004, 2006 The Linux Foundation and its contributors.

Everyone is permitted to copy and distribute verbatim copies of this
license document, but changing it is not allowed.


Developer's Certificate of Origin 1.1

By making a contribution to this project, I certify that:

(a) The contribution was created in whole or in part by me and I
    have the right to submit it under the open source license
    indicated in the file; or

(b) The contribution is based upon previous work that, to the best
    of my knowledge, is covered under an appropriate open source
    license and I have the right under that license to submit that
    work with modifications, whether created in whole or in part
    by me, under the same open source license (unless I am
    permitted to submit under a different license), as indicated
    in the file; or

(c) The contribution was provided directly to me by some other
    person who certified (a), (b) or (c) and I have not modified
    it.

(d) I understand and agree that this project and the contribution
    are public and that a record of the contribution (including all
    personal information I submit with it, including my sign-off) is
    maintained indefinitely and may be redistributed consistent with
    this project or the open source license(s) involved.
```

<!-- Put this here because this breaks KIO -->
<!-- SPDX-FileCopyrightText: (C) WithLithum & contributors 2023-2026 -->
<!-- SPDX-License-Identifier: Apache-2.0 -->