'use client';

import { Switch as MuiSwitch, useTheme } from '@mui/material';
import type { SwitchProps as MuiSwitchProps } from '@mui/material/Switch';
import type { ReactNode } from 'react';

type IconSwitchProps = Omit<MuiSwitchProps, 'sx'> & {
  checkedColor?: string;
  checkedThumbSvg: ReactNode;
  checkedTrackBackgroundColor?: string;
  iconHeight?: number;
  iconWidth?: number;
  thumbBackgroundColor?: string;
  thumbHoverBackgroundColor?: string;
  thumbSvg: ReactNode;
  trackBackgroundColor?: string;
};

const renderMuiSwitchThumb = (content: ReactNode) => (
  <span className="MuiSwitch-thumb">
    <span className="MuiSwitch-thumbContent">{content}</span>
  </span>
);

const IconSwitch = ({
  checkedColor,
  checkedThumbSvg,
  checkedTrackBackgroundColor,
  iconHeight,
  iconWidth,
  thumbBackgroundColor,
  thumbHoverBackgroundColor,
  thumbSvg,
  trackBackgroundColor,
  ...muiSwitchProps
}: IconSwitchProps) => {
  const theme = useTheme();

  const resolvedIconHeight = iconHeight ?? 18;
  const resolvedIconWidth = iconWidth ?? 18;
  const resolvedCheckedColor = checkedColor ?? theme.palette.common.white;
  const resolvedCheckedTrackBackgroundColor =
    checkedTrackBackgroundColor ?? theme.palette.grey[400];
  const resolvedThumbBackgroundColor = thumbBackgroundColor ?? theme.palette.primary.main;
  const resolvedThumbHoverBackgroundColor = thumbHoverBackgroundColor ?? theme.palette.primary.dark;
  const resolvedTrackBackgroundColor = trackBackgroundColor ?? theme.palette.grey[400];

  return (
    <MuiSwitch
      {...muiSwitchProps}
      icon={renderMuiSwitchThumb(thumbSvg)}
      checkedIcon={renderMuiSwitchThumb(checkedThumbSvg)}
      sx={{
        width: 62,
        height: 34,
        padding: 7,
        '& .MuiSwitch-switchBase': {
          margin: 1,
          padding: 0,
          transform: 'translateX(6px)',
          '&.Mui-checked': {
            color: resolvedCheckedColor,
            transform: 'translateX(22px)',
            '& + .MuiSwitch-track': {
              opacity: 1,
              backgroundColor: resolvedCheckedTrackBackgroundColor,
            },
          },
        },
        '& .MuiSwitch-thumb': {
          backgroundColor: resolvedThumbBackgroundColor,
          width: 32,
          height: 32,
          boxSizing: 'border-box',
          borderRadius: '50%',
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          overflow: 'hidden',
          transition: 'background-color 200ms',
        },
        '& .MuiSwitch-switchBase:hover .MuiSwitch-thumb': {
          backgroundColor: resolvedThumbHoverBackgroundColor,
        },
        '& .MuiSwitch-thumbContent': {
          display: 'flex',
          alignItems: 'center',
          justifyContent: 'center',
          width: '100%',
          height: '100%',
          lineHeight: 0,
        },
        '& .MuiSwitch-thumbContent > svg, & .MuiSwitch-thumbContent .MuiSvgIcon-root': {
          display: 'block',
          flexShrink: 0,
          width: resolvedIconWidth,
          height: resolvedIconHeight,
          fontSize: `${Math.max(resolvedIconWidth, resolvedIconHeight)}px`,
        },
        '& .MuiSwitch-track': {
          opacity: 1,
          backgroundColor: resolvedTrackBackgroundColor,
          borderRadius: 20 / 2,
        },
      }}
    />
  );
};

export default IconSwitch;
