import {
  DialogTitle,
  Menu,
  MenuItem,
} from '@mui/material';
import React, { Fragment } from 'react';
import ConfirmDialog from 'utils/ConfirmDialog';

interface MenuProps {
  open: boolean;
  anchorEl: null | HTMLElement;
  handleClose: () => void;
  handleDeleteMessage: () => void;
}

function OptionsMenu(props: MenuProps) {
  const { open, anchorEl, handleClose } = props;
  const [openDialog, setOpenDialog] = React.useState(false);

  const handleClickOpendialog = () => {
    props.handleClose();
    setOpenDialog(true);
  };

  const handleCloseDialog = () => {
    setOpenDialog(false);
  };

  return (
    <>
      <Menu
        id="options-menu"
        anchorEl={anchorEl}
        open={open}
        onClose={handleClose}
        PaperProps={{
          elevation: 0,
          sx: {
            overflow: 'visible',
            filter: 'drop-shadow(0px 2px 8px rgba(0,0,0,0.32))',
            mt: 1.5,
            '& .MuiAvatar-root': {
              width: 32,
              height: 32,
              ml: -0.5,
              mr: 1,
            },
            '&:before': {
              content: '""',
              display: 'block',
              position: 'absolute',
              top: 0,
              right: 12,
              width: 10,
              height: 10,
              bgcolor: 'background.paper',
              transform: 'translateY(-50%) rotate(45deg)',
              zIndex: 0,
            },
          },
        }}
        transformOrigin={{ horizontal: 'right', vertical: 'top' }}
        anchorOrigin={{ horizontal: 'right', vertical: 'bottom' }}
      >
        <MenuItem
          color="inherit"
          onClick={handleClickOpendialog}
          aria-haspopup="true"
          aria-controls="confirm-menu"
        >
          Xóa
        </MenuItem>
      </Menu>
      <ConfirmDialog id="confirm-menu" keepMounted open={openDialog} onClose={handleCloseDialog} handleSubmit={props.handleDeleteMessage}>
        <Fragment>
          <DialogTitle sx={{ textAlign: 'center' }}>Xác nhận xóa tin nhắn?</DialogTitle>
        </Fragment>
      </ConfirmDialog>
    </>
  );
}
export default React.memo(OptionsMenu);
