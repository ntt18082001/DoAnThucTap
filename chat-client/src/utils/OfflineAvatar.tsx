import { Badge, styled } from "@mui/material";

export const OfflineAvatar = styled(Badge)(({ theme }) => ({
  '& .MuiBadge-badge': {
    backgroundColor: '#9e9e9e',
    color: '#9e9e9e',
    boxShadow: `0 0 0 2px ${theme.palette.background.paper}`,
    minWidth: '10px',
    height: '10px',
    '&::after': {
      position: 'absolute',
      top: 0,
      left: 0,
      width: '100%',
      height: '100%',
      borderRadius: '50%',
      animation: 'ripple 1.2s infinite ease-in-out',
      border: '1px solid currentColor',
      content: '""',
    },
  }
}));