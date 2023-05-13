import { Box, Button } from '@mui/material';
import React from 'react';
import { CssLoadingButton } from 'utils/CssTextField';
import EmailIcon from '@mui/icons-material/Email';

interface ButtonStepProps {
  stepLength: number;
  activeStep: number;
  handleBack: () => void;
  isLoading?: boolean;
  type?: 'button' | 'submit';
}

function ButtonStep(props: ButtonStepProps) {
  const { activeStep, handleBack, stepLength, isLoading, type } = props;
  return (
    <Box sx={{ display: 'flex', flexDirection: 'row', pt: 2 }}>
      <Button
        color="info"
        disabled={activeStep === 0}
        variant="contained"
        onClick={handleBack}
        sx={{ mr: 1 }}
      >
        Trở lại
      </Button>
      <Box sx={{ flex: '1 1 auto' }} />
      <CssLoadingButton
        startIcon={<EmailIcon />}
        loadingPosition="start"
        loading={isLoading}
        color="secondary"
        type={type}
        variant="contained"
      >
        {activeStep === 0 && 'Gửi mã xác nhận'}
        {activeStep === 1 && 'Xác nhận mã'}
        {activeStep === stepLength - 1 && 'Hoàn thành'}
      </CssLoadingButton>
    </Box>
  );
}
export default React.memo(ButtonStep);
