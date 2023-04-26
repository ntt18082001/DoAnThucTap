import {
  InputAdornment,
  Paper,
  Popover,
  Typography,
} from '@mui/material';
import React, { useState } from 'react';
import { CssTextField } from 'utils/CssTextField';
import SearchIcon from '@mui/icons-material/Search';
import FindUserList from './FindUserList';
import { useDebounce } from 'hooks/useDebounce';

interface FindUserMsgProps {
  isOpen: boolean;
  anchorEl: HTMLButtonElement | null;
  handleHiddenPopper: () => void;
}

function FindUserMsg(props: FindUserMsgProps) {
  const [searchTerm, setSearchTerm] = useState('');
  const debounceSearchTerm = useDebounce(searchTerm, 1000);
  const id = props.isOpen ? 'simple-popover' : undefined;
  return (
    <Popover
        id={id}
        open={props.isOpen}
        anchorEl={props.anchorEl}
        onClose={props.handleHiddenPopper}
        anchorOrigin={{
          vertical: 'bottom',
          horizontal: 'left',
        }}
      >
          <Paper
            sx={{
              width: 420,
              maxHeight: 500,
              overflow: 'scroll',
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
              paddingTop: 1,
            }}
          >
            <Typography variant="h6" sx={{ fontWeight: '500' }}>
              Gửi tin nhắn mới
            </Typography>
            <CssTextField
              autoComplete='off'
              id="search"
              type="search"
              label="Tìm kiếm"
              value={searchTerm}
              onChange={(ev) => setSearchTerm(ev.target.value)}
              sx={{ width: 400 }}
              InputProps={{
                endAdornment: (
                  <InputAdornment position="end">
                    <SearchIcon color="secondary" />
                  </InputAdornment>
                ),
              }}
            />
            <FindUserList searchTerm={debounceSearchTerm} handleHiddenPopper={props.handleHiddenPopper} />
          </Paper>
        </Popover>
  );
}
export default React.memo(FindUserMsg);
