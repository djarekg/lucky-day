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
      main: '#9c55ff',
    },
    secondary: {
      main: '#f750aB',
    },
    container: '#1e1e1e',
  },
});

/** Exports the shared Material UI theme used by the app shell. */
export default theme;
