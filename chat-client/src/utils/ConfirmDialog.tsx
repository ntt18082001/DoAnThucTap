import { Button, Dialog, DialogActions } from '@mui/material';
import React, { ReactElement } from 'react';

export interface ConfirmationDialogRawProps {
  id?: string;
  keepMounted?: boolean;
  open: boolean;
  onClose: (value?: string) => void;
  children: ReactElement | ReactElement[];
  handleSubmit: () => void;
  titleSubmit?: string;
}

function ConfirmDialog(props: ConfirmationDialogRawProps) {
  const { onClose, open, handleSubmit, titleSubmit, ...other } = props;
  const handleCancel = () => {
    onClose();
  };

  return (
    <Dialog sx={{ '& .MuiDialog-paper': { maxHeight: 435 } }} maxWidth="xs" open={open} {...other}>
      {props.children}
      <DialogActions>
        <Button
          onClick={() => {
            handleSubmit();
            onClose();
          }}
        >
          {titleSubmit && titleSubmit}
          {!titleSubmit && 'Xóa'}
        </Button>
        <Button autoFocus onClick={handleCancel}>
          Hủy
        </Button>
      </DialogActions>
    </Dialog>
  );
}
export default React.memo(ConfirmDialog);
