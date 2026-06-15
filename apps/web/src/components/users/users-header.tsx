'use client';

import { makeStyles } from '@griffel/react';
import IconSwitch from '@lucky-day/components/icon-switch';
import GridViewIcon from '@mui/icons-material/GridView';
import TableViewIcon from '@mui/icons-material/TableView';
import Toolbar from '@mui/material/Toolbar';
import { useTheme } from '@mui/material/zero-styled';
import { useState } from 'react';

import { ViewMode } from '@/lib/models';

type UsersHeaderProps = {
  viewMode?: ViewMode;
  viewModeChange?: (mode: ViewMode) => void;
};

const useStyles = makeStyles({
  toolbar: {
    display: 'flex',
    justifyContent: 'flex-end',
    padding: '0.5rem 1rem',
  },
  header: {
    display: 'flex',
    justifyContent: 'center',
    alignItems: 'center',
    padding: '1rem',
  },
  viewMode: {
    marginLeft: '0.5rem',
    fontSize: '0.9rem',
    color: 'var(--mui-palette-text-secondary)',
    textTransform: 'uppercase',
  },
});

const UsersHeader = ({ viewMode = ViewMode.Card, viewModeChange }: UsersHeaderProps) => {
  const theme = useTheme();
  const styles = useStyles();
  const [isCardView, setIsCardView] = useState(viewMode === ViewMode.Card);

  const handleViewModeChange = () => {
    const newMode = isCardView ? ViewMode.Table : ViewMode.Card;
    setIsCardView(!isCardView);
    if (viewModeChange) {
      viewModeChange(newMode);
    }
  };

  return (
    <>
      <Toolbar className={styles.toolbar}>
        <IconSwitch
          thumbHoverBackgroundColor={theme.palette.primary.dark}
          thumbBackgroundColor={theme.palette.grey[900]}
          thumbSvg={<TableViewIcon />}
          checkedTrackBackgroundColor={theme.palette.primary.light}
          checkedThumbSvg={<GridViewIcon />}
          trackBackgroundColor={theme.palette.primary.main}
          tooltip="Table view"
          tooltipChecked="Card view"
          checked={isCardView}
          onChange={handleViewModeChange}
        />
      </Toolbar>
      <header className={styles.header}>
        <h1>
          Users <span className={styles.viewMode}>{isCardView ? 'Card View' : 'Table View'}</span>
        </h1>
      </header>
    </>
  );
};

export default UsersHeader;
