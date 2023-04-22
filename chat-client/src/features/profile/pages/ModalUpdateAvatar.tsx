import {
  Button,
  Card,
  CardMedia,
  Dialog,
  DialogActions,
  DialogContent,
  DialogTitle,
  IconButton,
  Stack,
} from '@mui/material';
import { useAppDispatch, useAppSelector } from 'app/hooks';
import { baseURL } from 'endpoints';
import { selectIsDarkmode } from 'features/darkmode/darkmodeSlice';
import useMode from 'hooks/useMode';
import React, { useCallback, useEffect, useState } from 'react';
import AddAPhotoIcon from '@mui/icons-material/AddAPhoto';
import BackupIcon from '@mui/icons-material/Backup';
import { CssLoadingButton } from 'utils/CssTextField';
import ClearIcon from '@mui/icons-material/Clear';
import { useUpdateAvatarMutation } from '../profile.service';
import { toast } from 'react-toastify';
import { setAvatar } from 'features/auth/authSlice';

interface ModalProps {
  isOpen: boolean;
  handleClose: () => void;
  urlAvatar?: string;
  id: string | undefined;
}

function ModalUpdateAvatar(props: ModalProps) {
  const dispatch = useAppDispatch();
  const isDarkmode = useAppSelector(selectIsDarkmode);
  const darkmode = useMode(isDarkmode);
  const [file, setFile] = useState<File | null>(null);
  const [updateAvatar, { data, isLoading, isSuccess }] = useUpdateAvatarMutation();

  const handleSubmitAvatar = async () => {
    if (file && props.id) {
      try {
        const formData = new FormData();
        formData.append('id', props.id);
        formData.append('file', file);
        await updateAvatar(formData).unwrap();
        props.handleClose();
        setFile(null);
      } catch (error) {
        console.log(error);
      }
    }
  };

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    if (e.target.files) {
      setFile(e.target.files[0]);
    }
  };

  const getImagePath = useCallback(() => {
    if (file) {
      return URL.createObjectURL(file);
    }
  }, [file]);

  useEffect(() => {
    if (isSuccess) {
      if (data) {
        dispatch(setAvatar(data.avatar));
        toast.success('Cập nhật ảnh đại diện thành công!');
      }
    }
  }, [isSuccess, data, dispatch]);

  return (
    <div>
      <Dialog
        open={props.isOpen}
        onClose={props.handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Sửa ảnh đại diện</DialogTitle>
        <DialogContent>
          <Card
            sx={{
              maxWidth: 400,
              width: 400,
              bgcolor: darkmode?.bgColor,
              color: darkmode?.color,
            }}
          >
            <CardMedia
              sx={{
                height: 350,
                display: 'flex',
                justifyContent: 'flex-end',
                alignItems: 'flex-end',
              }}
              image={file ? `${getImagePath()}` : `${baseURL}/${props.urlAvatar}`}
            >
              <Stack>
                <IconButton aria-label="upload picture" size="large" component="label">
                  <input hidden accept="image/*" type="file" onChange={handleFileChange} />
                  <AddAPhotoIcon fontSize="inherit" />
                </IconButton>
              </Stack>
            </CardMedia>
          </Card>
        </DialogContent>
        <DialogActions>
          <CssLoadingButton
            color="secondary"
            loadingPosition="start"
            startIcon={<BackupIcon />}
            variant="outlined"
            loading={isLoading}
            onClick={handleSubmitAvatar}
          >
            Lưu
          </CssLoadingButton>
          <Button
            color="info"
            variant="outlined"
            onClick={props.handleClose}
            startIcon={<ClearIcon />}
          >
            Hủy
          </Button>
        </DialogActions>
      </Dialog>
    </div>
  );
}

export default React.memo(ModalUpdateAvatar);
