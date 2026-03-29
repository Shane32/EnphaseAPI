# NuGetTemplate

A template repository for creating NuGet packages with GitHub Actions CI/CD, SourceLink support, and best-practice MSBuild configuration.

## 🚀 Getting Started

Follow these steps after creating a new repository from this template:

### 1. Create a new repository from this template

1. On GitHub, click the **Use this template** button at the top of this repository, then select **Create a new repository**.
2. Fill in the repository name, description, and visibility settings, then click **Create repository**.
3. Clone your new repository locally

### 2. Rename the solution and project files

Rename `NuGetTemplate.slnx` and `Project/Project.csproj` to match your package name, and update the solution file to reference the renamed project. For example, for a package named `MyCompany.MyLibrary`:

- `NuGetTemplate.slnx` → `MyCompany.MyLibrary.slnx`
- `Project/Project.csproj` → `Project/MyCompany.MyLibrary.csproj`

The `.csproj` filename determines the default NuGet package ID, so it must match the desired package name. Also update the `ProjectReference` in `Tests/Tests.csproj` to point to the renamed file.

### 3. Update `Directory.Build.props`

Edit [`Directory.Build.props`](Directory.Build.props) to set your package metadata:

```xml
<Copyright>Your Name</Copyright>
<Authors>Your Name</Authors>
<!-- Optional: add a logo image to the repo root and reference it here -->
<!-- <PackageIcon>logo.png</PackageIcon> -->
```

### 4. Update `LICENSE.txt`

Update [`LICENSE.txt`](LICENSE.txt) with your name and the current year. If you change the license type, also update the `PackageLicenseExpression` property in [`Directory.Build.props`](Directory.Build.props) to match (e.g. `MIT`, `Apache-2.0`). See [SPDX license identifiers](https://spdx.org/licenses/) for valid values.

### 5. Configure GitHub Actions secrets

The publish workflow will automatically push preview packages to **GitHub Packages** on every push to `master`. No secrets are required for this to work, but the repository must have **GitHub Packages** enabled (it is by default).

To also publish release packages to **NuGet.org**, add a repository secret named `NUGET_AUTH_TOKEN` containing your NuGet API key:

1. Go to [NuGet.org](https://www.nuget.org/) → Account → API Keys and create a key scoped to your package.
2. In your GitHub repository, go to **Settings → Secrets and variables → Actions → New repository secret**.
3. Name it `NUGET_AUTH_TOKEN` and paste your NuGet API key as the value.

### 6. Update this README

Replace this file with documentation for your package.

## ⚙️ CI/CD Workflows

This template includes two pre-configured GitHub Actions workflows. Both delegate to reusable workflows from [Shane32/SharedWorkflows](https://github.com/Shane32/SharedWorkflows) — see that repository for full implementation details.

### PR Tests (`.github/workflows/test.yml`)

Triggered on every **pull request**. Runs the following checks:

- **Format check** – verifies that all code is correctly formatted (via `dotnet format --verify-no-changes`).
- **Build check** – ensures the project builds without errors or warnings.
- **Tests** – runs the full test suite.

Concurrent runs for the same PR are automatically cancelled when a new commit is pushed.

### Build and Publish (`.github/workflows/publish.yml`)

Triggered on two events:

#### Push to `master` — preview packages

Every push to `master` produces a pre-release NuGet package and publishes it to **GitHub Packages**. The version is derived from the `VersionPrefix` property in [`Directory.Build.props`](Directory.Build.props) combined with the GitHub Actions run number. For example, with the default prefix of `1.0.0-preview`, the published version would be `1.0.0-preview-123` (where `123` is the run number).

To consume preview packages from GitHub Packages, add the feed to your NuGet configuration and authenticate with a personal access token.

#### GitHub Release — stable (or pre-release) packages

When you **publish a GitHub Release**, the workflow publishes a NuGet package using the release's **tag name** as the exact version. The tag name must be a valid NuGet version string — for example, `1.5.0` for a stable release or `1.5.0-beta.1` for a pre-release.

Steps to publish a release:

1. Decide on the version number (e.g. `1.5.0`).
2. In your GitHub repository, go to **Releases → Draft a new release**.
3. Set the **tag** to the exact version string (e.g. `1.5.0`). Create the tag on `master` (or the appropriate commit).
4. Fill in the release title and notes, then click **Publish release**.
5. The workflow will build and pack the project, then:
   - Push to **NuGet.org** if the `NUGET_AUTH_TOKEN` secret is configured.
   - Push to **GitHub Packages** if `NUGET_AUTH_TOKEN` is not configured.

## 📝 Template Notes

- **`IsPackable`** defaults to `false` for all projects. The main `Project/` project has it set to `true`. When adding a second project that should produce a NuGet package, add `<IsPackable>true</IsPackable>` to that project file. Test projects and other non-package projects should leave it as `false` (the default).

- **SourceLink** is automatically configured for all packable projects via `Microsoft.SourceLink.GitHub`. Ensure the repository is hosted on GitHub for this to work correctly.

- **`LICENSE.txt`**, **`README.md`**, and the logo image (when `PackageIcon` is set in `Directory.Build.props`) from the repository root are automatically included in all NuGet packages produced by packable projects.

- **`ContinuousIntegrationBuild`** is enabled automatically when running in GitHub Actions, which ensures deterministic builds and correct SourceLink paths.

## 📄 License

This template is licensed under the [MIT License](LICENSE.txt).

## 🙏 Credits

Glory to Jehovah, Lord of Lords and King of Kings, creator of Heaven and Earth, who through his Son Jesus Christ, has redeemed me to become a child of God. -[Shane32](https://github.com/Shane32)
