'use client';

import { Switch as MuiSwitch, Tooltip, useTheme } from '@mui/material';
import type { SwitchProps as MuiSwitchProps } from '@mui/material/Switch';
import { useState } from 'react';
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
  tooltip?: ReactNode;
  tooltipChecked?: ReactNode;
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
  tooltip,
  tooltipChecked,
  trackBackgroundColor,
  ...muiSwitchProps
}: IconSwitchProps) => {
  const theme = useTheme();

  const isControlled = muiSwitchProps.checked !== undefined;
  const [internalChecked, setInternalChecked] = useState(Boolean(muiSwitchProps.defaultChecked));
  const isChecked = isControlled ? Boolean(muiSwitchProps.checked) : internalChecked;

  const resolvedIconHeight = iconHeight ?? 18;
  const resolvedIconWidth = iconWidth ?? 18;
  const resolvedCheckedColor = checkedColor ?? theme.palette.common.white;
  const resolvedCheckedTrackBackgroundColor =
    checkedTrackBackgroundColor ?? trackBackgroundColor ?? theme.palette.grey[400];
  const resolvedThumbBackgroundColor = thumbBackgroundColor ?? theme.palette.primary.main;
  const resolvedThumbHoverBackgroundColor = thumbHoverBackgroundColor ?? theme.palette.primary.dark;
  const resolvedTrackBackgroundColor = trackBackgroundColor ?? theme.palette.grey[400];
  const thumbSize = 32;
  const trackHeight = 14;
  const tooltipTitle = isChecked ? (tooltipChecked ?? tooltip) : tooltip;

  const handleChange: MuiSwitchProps['onChange'] = (event, checked) => {
    if (!isControlled) {
      setInternalChecked(checked);
    }
    muiSwitchProps.onChange?.(event, checked);
  };

  return (
    <Tooltip
      title={tooltipTitle ?? ''}
      disableHoverListener={!tooltipTitle}
      disableFocusListener={!tooltipTitle}>
      <span>
        <MuiSwitch
          {...muiSwitchProps}
          onChange={handleChange}
          icon={renderMuiSwitchThumb(thumbSvg)}
          checkedIcon={renderMuiSwitchThumb(checkedThumbSvg)}
          sx={{
            width: 62,
            height: 40,
            padding: 0,
            '& .MuiSwitch-switchBase': {
              margin: 1,
              padding: 0,
              transform: 'translateX(0px)',
              '&.Mui-checked': {
                color: resolvedCheckedColor,
                transform: 'translateX(16px)',
                '& + .MuiSwitch-track': {
                  opacity: 1,
                  backgroundColor: resolvedCheckedTrackBackgroundColor,
                },
              },
            },
            '& .MuiSwitch-thumb': {
              backgroundColor: resolvedThumbBackgroundColor,
              width: thumbSize,
              height: thumbSize,
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
              position: 'absolute',
              top: (thumbSize - trackHeight) / 2,
              bottom: 0,
              left: 0,
              right: 0,
              margin: 'auto',
              width: thumbSize + 14,
              transform: 'translateX(2px)',
              height: trackHeight,
              boxSizing: 'border-box',
              borderRadius: trackHeight / 2,
            },
          }}
        />
      </span>
    </Tooltip>
  );
};

export default IconSwitch;
