import React, { ReactElement } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Modal from '@mui/material/Modal';

const style = {
  position: 'absolute' as 'absolute',
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 500,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  borderRadius: '8px'
};

interface ModalProps {
  isOpen: boolean;
  handleClose: () => void;
  children: ReactElement | ReactElement[];
  onSubmit?: () => void;
}

function ModalDialog(props: ModalProps) {
  return (
    <div>
      <Modal
        open={props.isOpen}
        onClose={props.handleClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          {props.children}
          {props.onSubmit && (
            <Box sx={{ textAlign: 'end' }}>
              <Button onClick={props.onSubmit}>Lưu</Button>
            </Box>
          )}
        </Box>
      </Modal>
    </div>
  );
}

export default React.memo(ModalDialog);
