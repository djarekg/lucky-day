import { defineConfig } from 'vite-plus';

/** Exports Vite+ settings for lint and type-aware checks. */
export default defineConfig({
  lint: { options: { typeAware: true, typeCheck: true } },
});
