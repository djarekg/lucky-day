import { defineConfig } from 'vite-plus';

/** Exports Vite+ settings for lint and type-aware checks. */
export default defineConfig({
  lint: { options: { typeAware: true, typeCheck: true } },
  ssr: {
    // Workaround for resolving dependencies in the server bundle
    // Without this, the React context will be different between direct import and transitive imports in development environment
    // For more information, see https://github.com/mui/material-ui/issues/45878#issuecomment-2987441663
    optimizeDeps: {
      include: ['@emotion/*', '@mui/*'],
    },
    noExternal: ['@emotion/*', '@mui/*'],
  },
});
