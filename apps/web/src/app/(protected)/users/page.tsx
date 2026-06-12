'use client';

import { IconSwitch } from '@lucky-day/components';
import GridViewIcon from '@mui/icons-material/GridView';
import TableViewIcon from '@mui/icons-material/TableView';
import { Toolbar, useTheme } from '@mui/material';

const Users = () => {
  const theme = useTheme();

  return (
    <>
      <Toolbar>
        <IconSwitch
          thumbHoverBackgroundColor={theme.palette.primary.dark}
          thumbBackgroundColor={theme.palette.grey[900]}
          thumbSvg={<TableViewIcon />}
          checkedTrackBackgroundColor={theme.palette.secondary.main}
          checkedThumbSvg={<GridViewIcon />}
        />
      </Toolbar>
      <div>
        <h1>Users</h1>
      </div>
    </>
  );
};

export default Users;
