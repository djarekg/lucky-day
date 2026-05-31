'use client';

import MenuOutlinedIcon from '@mui/icons-material/MenuOutlined';
import {
  Divider,
  Drawer,
  IconButton,
  ListItemButton,
  ListItemIcon,
  ListItemText,
  Toolbar,
} from '@mui/material';
import List from '@mui/material/List';
import { useState } from 'react';

import { navItems } from '@/components/site-nav/nav-items';

import styles from './site-nav.module.css';

const DEFAULT_DRAWER_WIDTH = 360;

const SiteNav = () => {
  const [open, setOpen] = useState(false);

  const toggleDrawer = () => {
    setOpen(!open);
  };

  return (
    <>
      <IconButton
        aria-label="site menu"
        onClick={toggleDrawer}>
        <MenuOutlinedIcon />
      </IconButton>
      <Drawer
        anchor="right"
        className={styles.drawer}
        open={open}
        onClose={toggleDrawer}
        sx={{
          '& .MuiDrawer-paper': {
            backgroundImage: 'none',
          },
        }}>
        <Toolbar />
        <Divider />
        <List
          aria-label="site navigation"
          component="nav"
          sx={{
            width: DEFAULT_DRAWER_WIDTH,
          }}>
          {navItems.map(item => (
            <ListItemButton
              key={item.label}
              component="a"
              href={item.href}>
              <ListItemIcon>{item.icon}</ListItemIcon>
              <ListItemText primary={item.label} />
            </ListItemButton>
          ))}
        </List>
      </Drawer>
    </>
  );
};

export default SiteNav;
