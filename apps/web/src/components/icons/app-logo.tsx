'use client';

import type { IconProps } from '@lucky-day/components';
import { useTheme } from '@mui/material/zero-styled';
import type { ComponentProps, FC } from 'react';

type AppLogoProps = ComponentProps<'svg'> &
  IconProps & {
    fill?: string;
    strokeWidth?: number;
    startColor?: string;
    endColor?: string;
  };

const AppLogoIcon: FC<AppLogoProps> = ({
  className,
  size = 32,
  strokeWidth = 1,
  startColor,
  endColor,
}) => {
  const theme = useTheme();
  startColor ??= theme.palette.primary.light;
  endColor ??= theme.palette.primary.dark;

  return (
    <svg
      data-icon="app-logo-icon"
      xmlns="http://www.w3.org/2000/svg"
      viewBox="0 0 32 32"
      className={`app-icon app-logo-icon ${className}`}
      fill="currentColor"
      width={`${size}px`}
      height={`${size}px`}>
      <title>App Logo Icon</title>
      <defs>
        <linearGradient
          id="appLogoIconGradient"
          x1="0%"
          y1="0%"
          x2="100%"
          y2="100%">
          <stop
            offset="0%"
            stopColor={startColor}
          />
          <stop
            offset="100%"
            stopColor={endColor}
          />
        </linearGradient>
      </defs>
      <path
        fill="url(#appLogoIconGradient)"
        strokeWidth={strokeWidth}
        stroke="url(#appLogoIconGradient)"
        d="M8.5 2a6.5 6.5 0 0 0 0 13H14a1 1 0 0 0 1-1V8.5A6.5 6.5 0 0 0 8.5 2m0 28a6.5 6.5 0 1 1 0-13H14a1 1 0 0 1 1 1v5.5A6.5 6.5 0 0 1 8.5 30m15-28a6.5 6.5 0 1 1 0 13H18a1 1 0 0 1-1-1V8.5A6.5 6.5 0 0 1 23.5 2m0 28a6.5 6.5 0 1 0 0-13H18a1 1 0 0 0-1 1v5.5a6.5 6.5 0 0 0 6.5 6.5"
      />
    </svg>
  );
};

export default AppLogoIcon;
