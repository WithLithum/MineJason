# Contributing Guide

Thank you for investing your time in contributing to our project!

Read the [Code of Conduct](CODE_OF_CONDUCT.md) of this project; it is the ground rules apply to this project, and it is enforced to keep this community approachable and respectable.

In this guide you will get an overview of the contribution workflow from reporting issues to contributing code by creating Merge Requests.

Use the table of contents icon on the top left corner of this document to get to a specific section of this guide quickly.

## Important

This is particularly important if you are seeing this from GitHub.

As GitHub MineJason repository is just a mirror, it does not accept contributions. Please refrain from trying to submit Pull Requests to it because it will most likely be ignored.

Please use [GitLab Merge Requests](https://gitlab.com/WithLithum/MineJason/-/merge-requests) and [GitLab Issues](https://gitlab.com/WithLithum/MineJason/-/issues) for contributions.

## Question of Problem?

Issues should not be use to submit general questions.

If you have questions about how to use MineJason or another general question related to it, please view the [our wiki](https://gitlab.com/WithLithum/MineJason/-/wikis/home) (WIP), or considering joining our:

- Discord server: [Click to join](https://discord.gg/UFfWb9Rj)
- QQ group: [Click to join (Chinese)](https://qm.qq.com/cgi-bin/qm/qr?k=reIRa9w7-vMBemqim7NdREX7vNKirNFo&jump_from=webapi&authKey=UnyZ5LWlfV8g8VCEffm2CShHd9PVPHP5CaXVbxkF2wwZj6FtXGEU/M7jRbU4e/K2)

## Reporting Issues

### Before reporting

Please search for existing _open_ issues that describes the issue you want to report. Please do not search for the whole issue description but rather look for keywords. Duplicate issues will be closed and linked to the currently tracked issue for the problem/request.

If an issue was previously reported and resolved, but it appeared again, please make a new report.

### When reporting

It is best that you would use the relevant issue template for the type of the issue you are reporting. You should use standard Chinese (as used) or English, and this does not include dialects.

Both Chinese and English versions of issue templates are provided. Use them appropriately, and please, do not mix Chinese template with English content, etc.

If you plan to work on the issue, please specify in the description. However, if you specify you plan to work on it but no Merge Requests have been submitted by you for a reasonably long period of time, we may end up choosing to open the issue for other contributions.

Below are the specific guidelines for each type of issue reports.

#### Bug Reports

Please describe the following things in your bug report as detailed as you can describe it:

- exact expected behaviour (the behaviour you would like to see)
- actual behaviour (what actually happened)
- steps to reproduce the issue

A proof of concept code would be the most helpful if it is am API behaviour issue.

Please make one issue for each bug, rather than reporting multiple bugs in one report.

#### Feature Request

Please, when submitting feature requests:

- Describe what is the feature, and what does it do?
- What is the intended use case of the feature?
- Are there any workarounds for the lacking of the feature?

The feature request will be evaluated and processed accordingly. However, do note that if the intended use case of the feature is not acceptable, then the feature request will be rejected. See [the Intended Use Cases wiki page](https://gitlab.com/WithLithum/MineJason/-/wikis/English/Development/Intended-Use-Case).

Please make an issue for each feature request rather than making several feature requests into a single issue.

#### Enhancement Request

Please, when submitting enhancement requests, describe what enhancement is needed?

Enhancement requests should not involve making breaking changes. If the requests involves breaking changes it will be evaluated for inclusion in the next major version.

### After reporting

Please note that all issues will be prioritised based on its urgency and importance.

Usually, critical bugs (like serious regressions, unexpected exceptions or major API unintended breaking change) will be fixed first.

## Submitting Code Contributions

The procedure for submitting code contributions is straight-forward,
but there are some considerations when submitting new features to the MineJason project.

By submitting code contributions you agree to the [Developer Certificate of Origin](#developer-certificate-of-origin). You are not necessary to add a `Signed-off-by` comment in your commits.

### Intended Use Cases

The intended use cases is to aid in programmatic creation of Raw JSON text format serialised text components to be used with "Notchian" Minecraft: Java Edition software.

Other use cases may be accepted but are not officially planned. See [the Intended Use Cases wiki page](https://gitlab.com/WithLithum/MineJason/-/wikis/English/Development/Intended-Use-Case).

### DO and DO NOTS

Please do:

- **DO** follow the [style guide](docs/STYLEGUIDE.md).
- **DO** give priority to the current style of the project or file you're changing even if it diverges from the general guidelines.
- **DO** include tests when adding new features. When fixing bugs, start with adding a test that highlights how the current behavior is broken.
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
- It requires proposal prior to Merge Request being created.

It is best that you open an issue with the proposed changes for breaking changes.

### Submitting Merge Requests

Prior to submitting merge requests for your first time as a beginner, please see [the GitLab guide on Merge Requests](https://docs.gitlab.com/ee/user/project/merge_requests/creating_merge_requests.html).

If you are not yet ready for the Merge Request to be reviewed and merged please make sure to make the Merge Request as a [Draft Merge Request](https://docs.gitlab.com/ee/user/project/merge_requests/drafts.html).

Please don't surprise us with big merge requests. Instead, file an issue and start a discussion so we can agree on a direction before you invest a large amount of time.

## Developer Certificate of Origin

By submitting code contributions you agree to the Developer Certificate of Origin:

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

This project does not require that you add `Signed-off-by` to your Git commits; contributing anything already signifies that you agrees with the DCO.
