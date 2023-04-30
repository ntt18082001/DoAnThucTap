import { Button, Dialog, DialogActions } from '@mui/material';
import React, { ReactElement } from 'react';

export interface ConfirmationDialogRawProps {
  id: string;
  keepMounted: boolean;
  open: boolean;
  onClose: (value?: string) => void;
  children: ReactElement | ReactElement[];
  handleSubmit: () => void;
}

function ConfirmDialog(props: ConfirmationDialogRawProps) {
  const { onClose, open, handleSubmit, ...other } = props;
  const handleCancel = () => {
    onClose();
  };

  return (
    <Dialog sx={{ '& .MuiDialog-paper': { maxHeight: 435 } }} maxWidth="xs" open={open} {...other}>
      {props.children}
      <DialogActions>
        <Button autoFocus onClick={handleCancel}>
          Hủy
        </Button>
        <Button
          onClick={() => {
            handleSubmit();
            onClose();
          }}
        >
          Xóa
        </Button>
      </DialogActions>
    </Dialog>
  );
}
export default React.memo(ConfirmDialog);
