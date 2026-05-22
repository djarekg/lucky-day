<!--VITE PLUS START-->

# Using Vite+, the Unified Toolchain for the Web

This project is using Vite+, a unified toolchain built on top of Vite, Rolldown, Vitest, tsdown, Oxlint, Oxfmt, and Vite Task. Vite+ wraps runtime management, package management, and frontend tooling in a single global CLI called `vp`. Vite+ is distinct from Vite, and it invokes Vite through `vp dev` and `vp build`. Run `vp help` to print a list of commands and `vp <command> --help` for information about a specific command.

Docs are local at `node_modules/vite-plus/docs` or online at https://viteplus.dev/guide/.

These instructions apply to work in this repository unless a more specific, path-scoped instruction overrides them.

## Review Checklist

- [ ] Run `vp install` after pulling remote changes and before getting started.
- [ ] Run `vp check` and `vp test` to format, lint, type check and test changes.
- [ ] If `vp install`, `vp check`, or `vp test` exits with a non-zero code, stop and report the full error output before proceeding with any further changes.
- [ ] Check if there are `vite.config.ts` tasks or `package.json` scripts necessary for validation, run via `vp run <script>`.
- [ ] For any new files or new data-fetching requirements added in `apps/web`, route API access through SWR-based data hooks and shared fetcher/key helpers. Do not introduce `fetch`/`useEffect` data loading in new components or new code paths, even when modifying existing files.

## Material UI theming

- For theme work in `apps/web`, use the Material UI theming skill as the source of truth: `skills/material-ui-theming/AGENTS.md`.
- If instructions in `skills/material-ui-theming/AGENTS.md` conflict with patterns found in `apps/web/src/styles/theme.ts` or `apps/web/src/app/layout.tsx`, follow `skills/material-ui-theming/AGENTS.md` and update those files to match it.
- Prefer `cssVariables: true`, `ThemeProvider`, `InitColorSchemeScript`, and `theme.vars` when editing the app shell, theme tokens, or color-scheme behavior.
- Treat `apps/web/src/styles/theme.ts` and `apps/web/src/app/layout.tsx` as the primary theming surfaces for SSR-safe Material UI changes.
- If a change touches dark mode, palette tokens, or flicker prevention, read `skills/material-ui-theming/AGENTS.md` in full before editing any component code.

<!--VITE PLUS END-->
