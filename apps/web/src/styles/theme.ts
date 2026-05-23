'use client';

import { createTheme } from '@mui/material/styles';

declare module '@mui/material/styles' {
  interface PaletteColor {
    container?: string;
  }

  interface PaletteOptions {
    container?: string;
  }
}

const theme = createTheme({
  cssVariables: true,
  typography: {
    fontFamily: 'var(--font-roboto)',
  },
  palette: {
    mode: 'dark',
    primary: {
      main: '#7223b6',
    },
    secondary: {
      main: '#f80094',
    },
    container: '#1e1e1e',
  },
});

/** Exports the shared Material UI theme used by the app shell. */
export default theme;
