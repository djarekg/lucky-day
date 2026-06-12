import GroupOutlinedIcon from '@mui/icons-material/GroupOutlined';
import HomeOutlinedIcon from '@mui/icons-material/HomeOutlined';

export type NavItem = {
  label: string;
  icon: React.ReactNode;
  href: string;
};

export const navItems: NavItem[] = [
  {
    label: 'Home',
    icon: <HomeOutlinedIcon />,
    href: '/',
  },
  {
    label: 'Users',
    icon: <GroupOutlinedIcon />,
    href: '/users',
  },
];
